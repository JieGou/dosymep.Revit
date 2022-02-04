using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles {
    /// <summary>
    /// Класс содержащий информацию о профилях.
    /// </summary>
    public class PlatformProfiles {
        /// <summary>
        /// Путь до корпоративного хранилища профилей.
        /// </summary>
        public static string ProfilePath => GetProfilePath();

        /// <summary>
        /// Путь до папок профилей у пользователя.
        /// </summary>
        public static string UserProfilesPath => GetUserProfilePath();

        /// <summary>
        /// Текущий выбранный экземпляр профиля.
        /// </summary>
        public static PlatformProfile Instance { get; internal set; }

        /// <summary>
        /// Наименование профиля по умолчанию.
        /// </summary>
        public string DefaultProfileName { get; set; }

        /// <summary>
        /// Список профилей.
        /// </summary>
        public List<PlatformProfile> Profiles { get; set; } = new List<PlatformProfile>();

        /// <summary>
        /// Сохранить настройки профилей.
        /// </summary>
        public void SavePlatformProfiles(string platformProfilePath) {
            if(string.IsNullOrEmpty(platformProfilePath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(platformProfilePath));
            }

            Directory.CreateDirectory(Path.GetDirectoryName(platformProfilePath));
            File.WriteAllText(platformProfilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        /// <summary>
        /// Возвращает профили платформы из удаленного местоположения.
        /// </summary>
        /// <returns>Возвращает профили платформы из удаленного местоположения.</returns>
        public static PlatformProfiles GetPlatformProfiles() {
            if(string.IsNullOrEmpty(ProfilePath)
               || !Directory.Exists(ProfilePath)) {
                return new PlatformProfiles();
            }

            string profilesFile = Path.Combine(ProfilePath, "Bim4Everyone.json");
            return !File.Exists(profilesFile)
                ? new PlatformProfiles()
                : JsonConvert.DeserializeObject<PlatformProfiles>(File.ReadAllText(profilesFile));
        }

        /// <summary>
        /// Возвращает профили платформы из пользовательского местоположения.
        /// </summary>
        /// <returns>Возвращает профили платформы из пользовательского местоположения.</returns>
        public static PlatformProfiles GetUserPlatformProfiles() {
            if(string.IsNullOrEmpty(UserProfilesPath)
               || !Directory.Exists(UserProfilesPath)) {
                return new PlatformProfiles();
            }

            string profilesFile = Path.Combine(UserProfilesPath, "Profiles.json");
            return !File.Exists(profilesFile)
                ? new PlatformProfiles()
                : JsonConvert.DeserializeObject<PlatformProfiles>(File.ReadAllText(profilesFile));
        }

        /// <summary>
        /// Загружает профили из удаленного местоположения.
        /// </summary>
        public static void DownloadProfiles() {
            PlatformProfiles platformProfiles = GetPlatformProfiles();
            foreach(string profilePath in platformProfiles.Profiles
                        .Where(profile => profile.ProfilePlace == ProfilePlace.Directory)
                        .Select(profile =>
                            Path.IsPathRooted(profile.ProfilesPath)
                                ? profile.ProfilesPath
                                : Path.Combine(ProfilePath, profile.ProfilesPath))) {

                CopyFilesRecursively(new DirectoryInfo(profilePath),
                    new DirectoryInfo(Path.Combine(UserProfilesPath, Path.GetFileName(profilePath))));
            }

            PlatformProfiles userPlatformProfiles = GetUserPlatformProfiles();
            
            userPlatformProfiles.Profiles = userPlatformProfiles.Profiles
                .Union(platformProfiles.Profiles)
                .ToList();
            
            userPlatformProfiles.SavePlatformProfiles(Path.Combine(UserProfilesPath, "Profiles.json"));
        }

        /// <summary>
        /// Загружает текущий профиль.
        /// </summary>
        public static void LoadCurrentProfile() {
            PlatformProfiles userPlatformProfiles = GetUserPlatformProfiles();
            if(string.IsNullOrEmpty(userPlatformProfiles.DefaultProfileName)) {
                PlatformProfiles platformProfiles = GetPlatformProfiles();
                userPlatformProfiles.DefaultProfileName = platformProfiles.DefaultProfileName;
            }

            Instance = userPlatformProfiles.Profiles
                           .FirstOrDefault(item => item.Name.Equals(userPlatformProfiles.DefaultProfileName))
                       ?? GetDefaultPlatformProfile();
            Instance.ProfilesPath = Path.Combine(UserProfilesPath, Instance.Name);
            Instance.LoadSettings();
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach(DirectoryInfo dir in source.GetDirectories()) {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach(FileInfo file in source.GetFiles()) {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }

        private static string GetProfilePath() {
            string profilesPath = Environment.GetEnvironmentVariable("BIM4EVERYONE_PROFILES");
            return string.IsNullOrEmpty(profilesPath)
                ? null
                : Path.Combine(profilesPath, "Bim4Everyone", ModuleEnvironment.RevitVersion);
        }

        private static string GetUserProfilePath() {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "pyrevit", "Bim4Everyone", ModuleEnvironment.RevitVersion);
        }

        private static PlatformProfile GetDefaultPlatformProfile() {
            return new PlatformProfile {
                Name = "dosymep", ProfilesPath = Path.Combine(ModuleEnvironment.CurrentLibraryPath, "profile")
            };
        }
    }
}
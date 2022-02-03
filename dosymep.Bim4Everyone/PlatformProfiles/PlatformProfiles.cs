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
        public List<PlatformProfile> Profiles { get; set; }

        /// <summary>
        /// Загружает профили из удаленного местоположения.
        /// </summary>
        public static void DownloadProfiles() {
            if(string.IsNullOrEmpty(ProfilePath)
               || !Directory.Exists(ProfilePath)) {
                Instance = GetDefaultPlatformProfile();
                Instance.LoadSettings();
                return;
            }

            string profilesFile = Path.Combine(ProfilePath, "Bim4Everyone.json");

            PlatformProfiles platformProfiles =
                JsonConvert.DeserializeObject<PlatformProfiles>(File.ReadAllText(profilesFile));

            foreach(string profilePath in platformProfiles.Profiles
                        .Where(profile => profile.ProfilePlace == ProfilePlace.Directory)
                        .Select(profile =>
                            Path.IsPathRooted(profile.ProfilesPath)
                                ? profile.ProfilesPath
                                : Path.Combine(ProfilePath, profile.ProfilesPath.ToString()))) {

                CopyFilesRecursively(new DirectoryInfo(profilePath),
                    new DirectoryInfo(Path.Combine(UserProfilesPath, Path.GetFileName(profilePath))));
            }

            Instance = platformProfiles.Profiles.FirstOrDefault(item =>
                item.Name.Equals(platformProfiles.DefaultProfileName));

            if(Instance == null) {
                Instance = GetDefaultPlatformProfile();
            } else {
                Instance.ProfilesPath = UserProfilesPath;
            }
            
            Instance.LoadSettings();
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach(DirectoryInfo dir in source.GetDirectories()) {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach(FileInfo file in source.GetFiles()) {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
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
using System;
using System.IO;

using dosymep.Bim4Everyone.KeySchedules;
using dosymep.Bim4Everyone.ProjectParams;
using dosymep.Bim4Everyone.Schedules;
using dosymep.Bim4Everyone.SharedParams;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles {
    /// <summary>
    /// Расположение путей профиля.
    /// </summary>
    public enum ProfilePlace {
        /// <summary>
        /// Папка на компьютере, либо на локальном сервере.
        /// </summary>
        Directory
    }

    /// <summary>
    /// Информация о профиле.
    /// </summary>
    public class PlatformProfile : IEquatable<PlatformProfile> {
        /// <summary>
        /// Наименование профиля.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на профиль.
        /// </summary>
        public string ProfilesPath { get; set; }

        /// <summary>
        /// Расположение папки профиля.
        /// </summary>
        public ProfilePlace ProfilePlace { get; set; }

        #region Paths

        /// <summary>
        /// Абсолютный путь до файла расширений pyrevit.
        /// </summary>
        [JsonIgnore]
        public string ExtensionsPath => Path.Combine(ProfilesPath, "Extensions.json");

        /// <summary>
        /// Абсолютный путь до файла настроек общих параметров проекта.
        /// </summary>
        [JsonIgnore]
        public string SharedParamsPath => Path.Combine(ProfilesPath, "RevitSharedParams.json");

        /// <summary>
        /// Абсолютный путь до файла настроек параметров проекта.
        /// </summary>
        [JsonIgnore]
        public string ProjectParamsPath => Path.Combine(ProfilesPath, "RevitProjectParams.json");

        /// <summary>
        /// Абсолютный путь до файла настроек спецификаций.
        /// </summary>
        [JsonIgnore]
        public string SchedulesPath => Path.Combine(ProfilesPath, "Schedules.json");

        /// <summary>
        /// Абсолютный путь до файла настроек ключевых спецификаций.
        /// </summary>
        [JsonIgnore]
        public string KeySchedulesPath => Path.Combine(ProfilesPath, "KeySchedules.json");

        /// <summary>
        /// Абсолютный путь до файла настроек параметров проекта.
        /// </summary>
        [JsonIgnore]
        public string PlatformSettingsPath =>
            Path.Combine(ProfilesPath, @"PlatformSettings\PlatformSettings.json");

        #endregion

        #region Settings

        /// <summary>
        /// Настройки общих параметров.
        /// </summary>
        [JsonIgnore]
        public SharedParamsConfig SharedParams => SharedParamsConfig.Instance;

        /// <summary>
        /// Настройки общих параметров проекта.
        /// </summary>
        [JsonIgnore]
        public ProjectParamsConfig ProjectParams => ProjectParamsConfig.Instance;

        /// <summary>
        /// Настройки спецификаций.
        /// </summary>
        [JsonIgnore]
        public SchedulesConfig Schedules => SchedulesConfig.Instance;

        /// <summary>
        /// Настройки платформы.
        /// </summary>
        [JsonIgnore]
        public PlatformSettings PlatformSettings => PlatformSettings.Instance;

        #endregion

        /// <summary>
        /// Загружает все настройки платформы.
        /// </summary>
        /// <returns></returns>
        public void LoadSettings() {
            // Сначала надо инициализировать параметры
            // потом спецификации, потому что они могут использовать параметры
            // потом плагины, потому что они могут использовать параметры и спецификации

            // Загрузка настроек параметров
            SharedParamsConfig.LoadInstance(SharedParamsPath);
            ProjectParamsConfig.LoadInstance(ProjectParamsPath);

            // Загрузка настроек спецификаций
            SchedulesConfig.LoadInstance(SchedulesPath);
            KeySchedulesConfig.LoadInstance(KeySchedulesPath);

            // Загрузка настроек плагинов
            PlatformSettings.LoadInstance(PlatformSettingsPath);
        }

        #region IEquatable

        /// <inheritdoc />
        public bool Equals(PlatformProfile other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return Name.Equals(other.Name);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }

            if(ReferenceEquals(this, obj)) {
                return true;
            }

            return Equals(obj as PlatformProfile);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        #endregion
    }
}
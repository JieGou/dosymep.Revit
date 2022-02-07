using System.Collections.Generic;
using System.IO;

using Autodesk.Revit.ApplicationServices;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles.PluginSettings {
    /// <summary>
    /// Пользовательские настройки платформы.
    /// </summary>
    public class UserPlatformSettings {
        /// <summary>
        /// Текущие пользовательские настройки платформы.
        /// </summary>
        public static UserPlatformSettings Instance { get; internal set; }

        /// <summary>
        /// Тема платформы.
        /// </summary>
        public Theme Theme { get; set; } = Theme.Light;
        
        /// <summary>
        /// Язык платформы.
        /// </summary>

        public LanguageType Language { get; set; } = LanguageType.English_USA;
        
        /// <summary>
        /// Дополнительные расширения.
        /// </summary>
        public List<string> ExternalExtensions { get; set; }
        
        /// <summary>
        /// Загрузка текущей конфигурации.
        /// </summary>
        /// <param name="configPath">Путь до конфигурации.</param>
        /// <remarks>Возвращает конфигурацию по умолчанию если был найден переданный файл.</remarks>
        public static void LoadInstance(string configPath) {
            Instance = Load(configPath);
        }

        /// <summary>
        /// Загрузка текущей конфигурации.
        /// </summary>
        /// <param name="configPath">Путь до конфигурации.</param>
        /// <remarks>Возвращает конфигурацию по умолчанию если был найден переданный файл.</remarks>
        public static UserPlatformSettings Load(string configPath) {
            return File.Exists(configPath)
                ? JsonConvert.DeserializeObject<UserPlatformSettings>(File.ReadAllText(configPath))
                : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static UserPlatformSettings GetDefaultConfig() {
            return new UserPlatformSettings();
        }
    }

    /// <summary>
    /// Темы
    /// </summary>
    public enum Theme {
        /// <summary>
        /// Темная
        /// </summary>
        Dark,
        
        /// <summary>
        /// Светлая
        /// </summary>
        Light
    }
}
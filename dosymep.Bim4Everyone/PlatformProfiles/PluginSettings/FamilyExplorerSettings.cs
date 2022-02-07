using System.IO;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles.PluginSettings {
    /// <summary>
    /// Класс настроек обозревателя семейств
    /// </summary>
    public class FamilyExplorerSettings {
        /// <summary>
        /// Текущие настройки обозревателя семейств.
        /// </summary>
        public static FamilyExplorerSettings Instance { get; internal set; }
        
        /// <summary>
        /// Путь до папки с настройками обозревателя семейств.
        /// </summary>
        public string FolderSettingsPath { get; set; }
        
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
        public static FamilyExplorerSettings Load(string configPath) {
            return File.Exists(configPath)
                ? JsonConvert.DeserializeObject<FamilyExplorerSettings>(File.ReadAllText(configPath))
                : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static FamilyExplorerSettings GetDefaultConfig() {
            return new FamilyExplorerSettings();
        }
    }
}
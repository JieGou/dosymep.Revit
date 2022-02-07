using System.IO;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles.PluginSettings {
    /// <summary>
    /// Настройки плагина копирования стандартов.
    /// </summary>
    public class CopyStandartsSettings {
        /// <summary>
        /// Текущие настройки копирования стандартов.
        /// </summary>
        public static CopyStandartsSettings Instance  { get; internal set; }
        
        /// <summary>
        /// Папка расположения файлов стандартов.
        /// </summary>
        public string StandartsFolderPath { get; set; }
        
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
        public static CopyStandartsSettings Load(string configPath) {
            return File.Exists(configPath)
                ? JsonConvert.DeserializeObject<CopyStandartsSettings>(File.ReadAllText(configPath))
                : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static CopyStandartsSettings GetDefaultConfig() {
            return new CopyStandartsSettings();
        }
    }
}
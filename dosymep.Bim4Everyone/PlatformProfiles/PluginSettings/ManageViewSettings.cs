using System.IO;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles.PluginSettings {
    /// <summary>
    /// Настройки менеджера видов.
    /// </summary>
    public class ManageViewSettings {
        /// <summary>
        /// Текущие настройки менеджера видов.
        /// </summary>
        public static ManageViewSettings Instance  { get; internal set; }
        
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
        public static ManageViewSettings Load(string configPath) {
            return File.Exists(configPath)
                ? JsonConvert.DeserializeObject<ManageViewSettings>(File.ReadAllText(configPath))
                : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static ManageViewSettings GetDefaultConfig() {
            return new ManageViewSettings();
        }
    }
}
using System.IO;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.PlatformProfiles {
    /// <summary>
    /// Класс конфигурации платформы.
    /// </summary>
    public class PlatformSettings {
        /// <summary>
        /// Текущие настройки платформы.
        /// </summary>
        public static PlatformSettings Instance { get; internal set; }

        /// <summary>
        /// Абсолютный путь до файла шаблона пустого проекта.
        /// </summary>
        public string EmptyProjectFile { get; set; }

        /// <summary>
        /// Абсолютный путь до файла общих параметров.
        /// </summary>
        public string SharedParamsFilePath { get; set; }

        /// <summary>
        /// Абсолютный путь до файла параметров проекта.
        /// </summary>
        public string ProjectParamsFilePath { get; set; }

        /// <summary>
        /// Абсолютный пть до файла со спецификациями.
        /// </summary>
        public string SchedulesFilePath { get; set; }

        /// <summary>
        /// Абсолютный пть до файла с ключевыми спецификациями.
        /// </summary>
        public string KeySchedulesFilePath { get; set; }

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
        public static PlatformSettings Load(string configPath) {
            return File.Exists(configPath)
                ? JsonConvert.DeserializeObject<PlatformSettings>(File.ReadAllText(configPath))
                : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static PlatformSettings GetDefaultConfig() {
            string modulePath = ModuleEnvironment.CurrentLibraryPath;
            return new PlatformSettings() {
                EmptyProjectFile = Path.Combine(modulePath, @"templates\empty_project.rte"),
                SharedParamsFilePath = Path.Combine(modulePath, @"templates\shared_parameters.txt"),
                ProjectParamsFilePath = Path.Combine(modulePath, @"templates\project_parameters.rvt"),
                SchedulesFilePath =  Path.Combine(modulePath, @"templates\project_parameters.rvt"),
                KeySchedulesFilePath =  Path.Combine(modulePath, @"templates\project_parameters.rvt"),
            };
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.SharedParams {
    /// <summary>
    /// Конфигурация общих параметров.
    /// </summary>
    public class SharedParamsConfig {
        /// <summary>
        /// Текущее состояние конфигурации.
        /// </summary>
        /// <remarks>Перед использованием нужно вызвать <see cref="Load(string)"/></remarks>
        public static SharedParamsConfig Instance { get; internal set; }

        /// <summary>
        /// Размер-ширина
        /// </summary>
        public SharedParam SizeWidth { get; internal set; } = new SharedParam() { PropertyName = nameof(SizeWidth), Name = "Speech_Размер_Ширина" };

        /// <summary>
        /// Размер-глубина
        /// </summary>
        public SharedParam SizeDepth { get; internal set; } = new SharedParam() { PropertyName = nameof(SizeDepth), Name = "Speech_Размер_Глубина" };

        /// <summary>
        /// Перемычка-существование
        /// </summary>
        public SharedParam BulkheadExists { get; internal set; } = new SharedParam() { PropertyName = nameof(BulkheadExists), Name = "Наличие Перемычки" };

        /// <summary>
        /// Перемычка-длина
        /// </summary>
        public SharedParam BulkheadLength { get; internal set; } = new SharedParam() { PropertyName = nameof(BulkheadLength), Name = "Перемычка Длина" };

        /// <summary>
        /// Перемычка-глубина
        /// </summary>
        public SharedParam BulkheadDepth { get; internal set; } = new SharedParam() { PropertyName = nameof(BulkheadDepth), Name = "Перемычка Глубина" };

        /// <summary>
        /// Перемычка-класс
        /// </summary>
        public SharedParam BulkheadClass { get; internal set; } = new SharedParam() { PropertyName = nameof(BulkheadClass), Name = "Перемычка Класс" };

        /// <summary>
        /// Сохранение текущей конфигурации.
        /// </summary>
        /// <param name="configPath">Путь до конфигурации.</param>
        public void Save(string configPath) {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        /// <summary>
        /// Возвращает весь список настроек общих параметров.
        /// </summary>
        /// <returns>Возвращает весь список настроек общих параметров.</returns>
        public IEnumerable<SharedParam> GetSharedParams() {
            return GetType().GetProperties().Select(item => item.GetValue(this)).OfType<SharedParam>().OrderBy(item => item.PropertyName);
        }

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
        public static SharedParamsConfig Load(string configPath) {
            return File.Exists(configPath) ? JsonConvert.DeserializeObject<SharedParamsConfig>(File.ReadAllText(configPath)) : GetDefaultConfg();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static SharedParamsConfig GetDefaultConfg() {
            return new SharedParamsConfig();
        }
    }
}

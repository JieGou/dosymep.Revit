﻿using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

using dosymep.Bim4Everyone.SimpleServices;
using dosymep.Revit;

namespace dosymep.Bim4Everyone.SystemParams {

    /// <summary>
    /// Configuration of system parameters.
    /// </summary>
    public class SystemParamsConfig : RevitParamsConfig, ISystemParamsService {
        private readonly LanguageType? _languageType;

        /// <summary>
        /// Initializes the configuration of system parameters.
        /// </summary>
        internal SystemParamsConfig() {
            _revitParams = new Dictionary<string, RevitParam>();
        }

        /// <summary>
        /// Initializes the configuration of system parameters.
        /// </summary>
        /// <param name="languageType">Язык системы.</param>
        internal SystemParamsConfig(LanguageType? languageType)
            : this() {
            _languageType = languageType;
        }

        /// <summary>
        /// Текущее состояние конфигурации.
        /// </summary>
        /// <remarks>Перед использованием нужно вызвать <see cref="LoadInstance(Autodesk.Revit.ApplicationServices.LanguageType?)"/></remarks>
        public static SystemParamsConfig Instance { get; internal set; }

        /// <inheritdoc/>
        [Obsolete]
        public SystemParam CreateRevitParam(BuiltInParameter systemParamId) {
            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(_languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, BuiltInParameter systemParamId) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(document, _languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, BuiltInParameter systemParamId,
            LanguageType languageType) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(document, languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam this[BuiltInParameter paramId]
            => (SystemParam) this[paramId.ToString()];

        /// <inheritdoc/>
        IEnumerable<SystemParam> ISystemParamsService.GetRevitParams() {
            return base.GetRevitParams().Cast<SystemParam>();
        }

        [Obsolete]
        private SystemParam CreateRevitParam(LanguageType? languageType, string paramId) {
            return string.IsNullOrEmpty(paramId)
                ? null
                : new SystemParam(paramId) {
                    LanguageType = languageType,
                    StorageType = SystemParam.GetSystemParamId(paramId).GetStorageType()
                };
        }

        private SystemParam CreateRevitParam(Document document, LanguageType? languageType, string paramId) {
            return string.IsNullOrEmpty(paramId)
                ? null
                : new SystemParam(paramId) {
                    LanguageType = languageType,
                    StorageType = document.GetStorageType(SystemParam.GetSystemParamId(paramId))
                };
        }

        private string GetParamId(BuiltInParameter systemParamId) {
            return Enum.GetName(typeof(BuiltInParameter), systemParamId);
        }

#if REVIT2022_OR_GREATER

        /// <inheritdoc/>
        public SystemParam this[ForgeTypeId paramId]
            => (SystemParam) this[GetParamId(paramId)];

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, ForgeTypeId systemParamId) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(systemParamId == null) {
                throw new ArgumentNullException(nameof(systemParamId));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(document, _languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, ForgeTypeId systemParamId, LanguageType languageType) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(systemParamId == null) {
                throw new ArgumentNullException(nameof(systemParamId));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(document, languageType, paramId);
        }

        private string GetParamId(ForgeTypeId systemParamId) {
#pragma warning disable CS0618
            return Enum.GetName(typeof(BuiltInParameter), ParameterUtils.GetBuiltInParameter(systemParamId));
#pragma warning restore CS0618
        }

#endif

        /// <inheritdoc/>
        SystemParam ISystemParamsService.this[string paramId]
            => (SystemParam) this[paramId];

        /// <inheritdoc/>
        public override void Save(string configPath) {
            if(string.IsNullOrEmpty(configPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(configPath));
            }

            throw new NotSupportedException("Сохранение конфигурации для системных параметров бесполезно.");
        }

        /// <summary>
        /// Загрузка текущей конфигурации.
        /// </summary>
        /// <param name="languageType">Язык системы.</param>
        public static void LoadInstance(LanguageType? languageType) {
            Instance = languageType.HasValue ? Load(languageType) : GetDefaultConfig();
        }

        /// <summary>
        /// Загрузка текущей конфигурации.
        /// </summary>
        /// <param name="languageType">Язык системы.</param>
        public static SystemParamsConfig Load(LanguageType? languageType) {
            return new SystemParamsConfig(languageType);
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static SystemParamsConfig GetDefaultConfig() {
            return new SystemParamsConfig();
        }
    }
}
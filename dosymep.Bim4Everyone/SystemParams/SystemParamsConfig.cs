﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

using dosymep.Bim4Everyone.SimpleServices;
using dosymep.Revit;

namespace dosymep.Bim4Everyone.SystemParams {
    /// <summary>
    /// Конфигурация системных параметров.
    /// </summary>
    public class SystemParamsConfig : RevitParamsConfig, ISystemParamsService {
        private readonly LanguageType? _languageType;

        /// <summary>
        /// Инициализирует конфигурацию системных параметров.
        /// </summary>
        internal SystemParamsConfig() { }

        /// <summary>
        /// Инициализирует конфигурацию системных параметров.
        /// </summary>
        /// <param name="languageType">Язык системы.</param>
        internal SystemParamsConfig(LanguageType? languageType) {
            _languageType = languageType;
        }

        /// <summary>
        /// Текущее состояние конфигурации.
        /// </summary>
        /// <remarks>Перед использованием нужно вызвать <see cref="LoadInstance(Autodesk.Revit.ApplicationServices.LanguageType?)"/></remarks>
        public static SystemParamsConfig Instance { get; internal set; }

        /// <inheritdoc/>
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
            return CreateRevitParam(_languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(BuiltInParameter systemParamId, LanguageType languageType) {
            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(languageType, paramId);
        }
        
        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, BuiltInParameter systemParamId, LanguageType languageType) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(languageType, paramId);
        }

        /// <inheritdoc/>
        public override RevitParam this[string paramId] {
            get {
                if(string.IsNullOrEmpty(paramId)) {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(paramId));
                }

                if(HasParamId(paramId)) {
                    return CreateRevitParam(_languageType, paramId);
                }

                return null;
            }
        }

        /// <inheritdoc/>
        public SystemParam this[BuiltInParameter paramId] 
            => CreateRevitParam(_languageType, paramId);

        /// <inheritdoc/>
        IEnumerable<SystemParam> ISystemParamsService.GetRevitParams() {
            return Enum.GetValues(typeof(BuiltInParameter))
                .Cast<BuiltInParameter>()
                .Select(item => CreateRevitParam(_languageType, item));
        }

        /// <inheritdoc/>
        public override IEnumerable<RevitParam> GetRevitParams() {
            return Enum.GetValues(typeof(BuiltInParameter))
                .Cast<BuiltInParameter>()
                .Select(item => CreateRevitParam(_languageType, item));
        }
        
        private SystemParam CreateRevitParam(LanguageType? languageType, string paramId) {
            return string.IsNullOrEmpty(paramId) ? null : new SystemParam(languageType, paramId);
        }
        
        private SystemParam CreateRevitParam(LanguageType? languageType, BuiltInParameter systemParamId) {
            string paramId = GetParamId(systemParamId);
            return new SystemParam(languageType, paramId);
        }
        
        private string GetParamId(BuiltInParameter systemParamId) {
            return Enum.GetName(typeof(BuiltInParameter), systemParamId);
        }
        
        private bool HasParamId(string paramId) {
            return string.IsNullOrEmpty(paramId)
                ? false
                : Enum.IsDefined(typeof(BuiltInParameter), paramId);
        }
        
#if D2022 || R2022
        
        /// <inheritdoc/>
        public SystemParam this[ForgeTypeId paramId] 
            => CreateRevitParam(_languageType, paramId);
        
        /// <inheritdoc/>
        public SystemParam CreateRevitParam(ForgeTypeId systemParamId) {
            if(systemParamId == null) {
                throw new ArgumentNullException(nameof(systemParamId));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(_languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(Document document, ForgeTypeId systemParamId) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(systemParamId == null) {
                throw new ArgumentNullException(nameof(systemParamId));
            }

            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(_languageType, paramId);
        }

        /// <inheritdoc/>
        public SystemParam CreateRevitParam(ForgeTypeId systemParamId, LanguageType languageType) {
            if(systemParamId == null) {
                throw new ArgumentNullException(nameof(systemParamId));
            }
            
            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(languageType, paramId);
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
            return CreateRevitParam(languageType, paramId);
        }
        
        private SystemParam CreateRevitParam(LanguageType? languageType, ForgeTypeId systemParamId) {
            string paramId = GetParamId(systemParamId);
            return CreateRevitParam(languageType, paramId);
        }

        private string GetParamId(ForgeTypeId systemParamId) {
            return Enum.GetName(typeof(BuiltInParameter), ParameterUtils.GetBuiltInParameter(systemParamId));
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
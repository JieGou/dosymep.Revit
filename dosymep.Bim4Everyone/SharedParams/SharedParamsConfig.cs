﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using dosymep.Bim4Everyone.SimpleServices;
using dosymep.Revit;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.SharedParams {
    /// <summary>
    /// Конфигурация общих параметров.
    /// </summary>
    public class SharedParamsConfig : RevitParamsConfig, ISharedParamsService {
        /// <summary>
        /// Текущее состояние конфигурации.
        /// </summary>
        /// <remarks>Перед использованием нужно вызвать <see cref="LoadInstance(string)"/></remarks>
        public static SharedParamsConfig Instance { get; internal set; }

        #region Размер

        /// <summary>
        /// Размер-ширина
        /// </summary>
        public SharedParam SizeWidth { get; internal set; } = new SharedParam() { PropertyName = nameof(SizeWidth), Name = "Speech_Размер_Ширина" };

        /// <summary>
        /// Размер-глубина
        /// </summary>
        public SharedParam SizeDepth { get; internal set; } = new SharedParam() { PropertyName = nameof(SizeDepth), Name = "Speech_Размер_Глубина" };

        #endregion

        #region Перемычка

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

        #endregion

        #region

        /// <summary>
        /// ADSK_Комплект чертежей
        /// </summary>
        public SharedParam AlbumBlueprints { get; internal set; } = new SharedParam() { PropertyName = nameof(AlbumBlueprints), Name = "ADSK_Комплект чертежей" };

        /// <summary>
        /// ADSK_Имя системы
        /// </summary>
        public SharedParam MechanicalSystemName { get; internal set; } = new SharedParam() { PropertyName = nameof(MechanicalSystemName), Name = "ADSK_Имя системы" };


        #endregion

        #region Штамп

        /// <summary>
        /// Ш.НомерЛиста
        /// </summary>
        public SharedParam StampSheetNumber { get; internal set; } = new SharedParam() { PropertyName = nameof(StampSheetNumber), Name = "Ш.НомерЛиста" };

        #endregion

        #region Квартирография

        /// <summary>
        /// ФОП_ПМЩ_Площадь
        /// </summary>
        public SharedParam RoomArea { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomArea), Name = "ФОП_ПМЩ_Площадь" };

        /// <summary>
        /// ФОП_ПМЩ_Площадь с коэф.
        /// </summary>
        public SharedParam RoomAreaWithRatio { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomAreaWithRatio), Name = "ФОП_ПМЩ_Площадь с коэф." };

        /// <summary>
        /// ФОП_КВР_Площадь с коэф.
        /// </summary>
        public SharedParam ApartmentAreaRatio { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaRatio), Name = "ФОП_КВР_Площадь с коэф." };

        /// <summary>
        /// ФОП_КВР_Площадь без ЛП
        /// </summary>
        public SharedParam ApartmentAreaNoBalcony { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaNoBalcony), Name = "ФОП_КВР_Площадь без ЛП" };

        /// <summary>
        /// ФОП_КВР_Площадь жилая
        /// </summary>
        public SharedParam ApartmentLivingArea { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentLivingArea), Name = "ФОП_КВР_Площадь жилая" };

        /// <summary>
        /// ФОП_КВР_Площадь без коэф.
        /// </summary>
        public SharedParam ApartmentArea { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentArea), Name = "ФОП_КВР_Площадь без коэф." };

        /// <summary>
        /// ФОП_КВР_Площадь по пятну
        /// </summary>
        public SharedParam ApartmentFullArea { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentFullArea), Name = "ФОП_КВР_Площадь по пятну" };

        /// <summary>
        /// ФОП_Номер квартиры
        /// </summary>
        public SharedParam ApartmentNumber { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentNumber), Name = "ФОП_Номер квартиры" };

        /// <summary>
        /// ФОП_Доп. номер помещения
        /// </summary>
        public SharedParam ApartmentNumberExtra { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentNumberExtra), Name = "ФОП_Доп. номер помещения" };

        /// <summary>
        /// ФОП_Количество комнат
        /// </summary>
        public SharedParam RoomsCount { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomsCount), Name = "ФОП_Количество комнат" };

        /// <summary>
        /// ФОП_ФИКС_КВР_Площадь с коэф.
        /// </summary>
        public SharedParam ApartmentAreaRatioFix { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaRatioFix), Name = "ФОП_ФИКС_КВР_Площадь с коэф." };

        /// <summary>
        /// ФОП_ФИКС_КВР_Площадь без ЛП
        /// </summary>
        public SharedParam ApartmentAreaNoBalconyFix { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaNoBalconyFix), Name = "ФОП_ФИКС_КВР_Площадь без ЛП" };

        /// <summary>
        /// ФОП_ФИКС_КВР_Площадь жилая
        /// </summary>
        public SharedParam ApartmentLivingAreaFix { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentLivingAreaFix), Name = "ФОП_ФИКС_КВР_Площадь жилая" };

        /// <summary>
        /// ФОП_ФИКС_КВР_Площадь без коэф.
        /// </summary>
        public SharedParam ApartmentAreaFix { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaFix), Name = "ФОП_ФИКС_КВР_Площадь без коэф." };

        /// <summary>
        /// ФОП_ФИКС_ПМЩ_Площадь
        /// </summary>
        public SharedParam RoomAreaFix { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomAreaFix), Name = "ФОП_ФИКС_ПМЩ_Площадь" };

        /// <summary>
        /// ФОП_ФИКС_ПМЩ_Площадь с коэф.
        /// </summary>
        public SharedParam RoomAreaWithRatioFix { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomAreaWithRatioFix), Name = "ФОП_ФИКС_ПМЩ_Площадь с коэф." };

        /// <summary>
        /// ФОП_ФИКС_КВР_Площадь по пятну
        /// </summary>
        public SharedParam ApartmentFullAreaFix { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentFullAreaFix), Name = "ФОП_ФИКС_КВР_Площадь по пятну" };

        /// <summary>
        /// ФОП_Коэффициент площади
        /// </summary>
        public SharedParam RoomAreaRatio { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomAreaRatio), Name = "ФОП_Коэффициент площади" };

        /// <summary>
        /// ФОП_Доп. сортировка групп
        /// </summary>
        public SharedParam ApartmentGroupName { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentGroupName), Name = "ФОП_Доп. сортировка групп" };

        /// <summary>
        /// ФОП_ПМЩ_Группа
        /// </summary>
        public SharedParam RoomGroupShortName { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomGroupShortName), Name = "ФОП_ПМЩ_Группа" };

        /// <summary>
        /// ФОП_Пожарный отсек
        /// </summary>
        public SharedParam FireCompartmentShortName { get; internal set; } = new SharedParam() { PropertyName = nameof(FireCompartmentShortName), Name = "ФОП_Пожарный отсек" };

        /// <summary>
        /// ФОП_ПМЩ_Секция
        /// </summary>
        public SharedParam RoomSectionShortName { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomSectionShortName), Name = "ФОП_ПМЩ_Секция" };

        /// <summary>
        /// ФОП_Тип группы помещений
        /// </summary>
        public SharedParam RoomTypeGroupShortName { get; internal set; } = new SharedParam() { PropertyName = nameof(RoomTypeGroupShortName), Name = "ФОП_Тип группы помещений" };

        /// <summary>
        /// ФОП_КВР_Площадь по ТЗ
        /// </summary>
        public SharedParam ApartmentAreaSpec { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaSpec), Name = "ФОП_КВР_Площадь по ТЗ" };

        /// <summary>
        /// ФОП_КВР_Площадь по ТЗ мин
        /// </summary>
        public SharedParam ApartmentAreaMinSpec { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaMinSpec), Name = "ФОП_КВР_Площадь по ТЗ мин" };

        /// <summary>
        /// ФОП_КВР_Площадь по ТЗ макс
        /// </summary>
        public SharedParam ApartmentAreaMaxSpec { get; internal set; } = new SharedParam() { PropertyName = nameof(ApartmentAreaMaxSpec), Name = "ФОП_КВР_Площадь по ТЗ макс" };

        #endregion

        #region Секции

        /// <summary>
        /// ФОП_Этаж
        /// </summary>
        public SharedParam Level { get; internal set; } = new SharedParam() { PropertyName = nameof(Level), Name = "ФОП_Этаж" };

        /// <summary>
        /// ФОП_Номер секции
        /// </summary>
        public SharedParam BuildingWorksSection { get; internal set; } = new SharedParam() { PropertyName = nameof(BuildingWorksSection), Name = "ФОП_Секция СМР" };

        /// <summary>
        /// ФОП_Блок СМР
        /// </summary>
        public SharedParam BuildingWorksBlock { get; internal set; } = new SharedParam() { PropertyName = nameof(BuildingWorksBlock), Name = "ФОП_Блок СМР" };

        /// <summary>
        /// ФОП_Экономическая функция
        /// </summary>
        public SharedParam EconomicFunction { get; internal set; } = new SharedParam() { PropertyName = nameof(EconomicFunction), Name = "ФОП_Экономическая функция" };

        #endregion

        #region ВИС
        
        /// <summary>
        /// ФОП_ВИС_Группирование
        /// </summary>
        public SharedParam VISGrouping { get; internal set; } = new SharedParam() { PropertyName = nameof(VISGrouping), Name = "ФОП_ВИС_Группирование" };
        
        /// <summary>
        /// ФОП_ВИС_Масса
        /// </summary>
        public SharedParam VISMass { get; internal set; } = new SharedParam() { PropertyName = nameof(VISMass), Name = "ФОП_ВИС_Масса" };
        
        /// <summary>
        /// ФОП_ВИС_Единица измерения
        /// </summary>
        public SharedParam VISUnit { get; internal set; } = new SharedParam() { PropertyName = nameof(VISUnit), Name = "ФОП_ВИС_Единица измерения" };
        
        /// <summary>
        /// ФОП_ВИС_Минимальная толщина воздуховода
        /// </summary>
        public SharedParam VISMinDuctThickness { get; internal set; } = new SharedParam() { PropertyName = nameof(VISMinDuctThickness), Name = "ФОП_ВИС_Минимальная толщина воздуховода" };
        
        /// <summary>
        /// ФОП_ВИС_Наименование комбинированное
        /// </summary>
        public SharedParam VISCombinedName { get; internal set; } = new SharedParam() { PropertyName = nameof(VISCombinedName), Name = "ФОП_ВИС_Наименование комбинированное" };
        
        /// <summary>
        /// ФОП_ВИС_Число
        /// </summary>
        public SharedParam VISSpecNumbers { get; internal set; } = new SharedParam() { PropertyName = nameof(VISSpecNumbers), Name = "ФОП_ВИС_Число" };
        
        /// <summary>
        /// ФОП_ВИС_Экономическая функция
        /// </summary>
        public SharedParam VISEconomicFunction { get; internal set; } = new SharedParam() { PropertyName = nameof(VISEconomicFunction), Name = "ФОП_ВИС_Экономическая функция" };

        #endregion
        
        /// <inheritdoc />
        public SharedParam GetRevitParam(Document document, string revitParamName) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(string.IsNullOrEmpty(revitParamName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(revitParamName));
            }

            return GetRevitParam(document, revitParamName, document.GetSharedParam(revitParamName));
        }

        /// <inheritdoc />
        public SharedParam GetRevitParam(Document document, ParameterElement revitParamElement) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(revitParamElement == null) {
                throw new ArgumentNullException(nameof(revitParamElement));
            }
            
            return GetRevitParam(document, revitParamElement.Name, revitParamElement);
        }
        
        /// <inheritdoc />
        public SharedParam GetRevitParam(Document document, string propertyName, string revitParamName) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(string.IsNullOrEmpty(revitParamName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(revitParamName));
            }

            return GetRevitParam(document, propertyName, document.GetSharedParam(revitParamName));
        }

        /// <inheritdoc />
        public SharedParam GetRevitParam(Document document, string propertyName, ParameterElement revitParamElement) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(revitParamElement == null) {
                throw new ArgumentNullException(nameof(revitParamElement));
            }

            var binding = document.GetParameterBindings()
                .FirstOrDefault(item => item.Definition.Name.Equals(revitParamElement.Name));
            
            if(binding.Binding == null) {
                return new SharedParam(StorageType.None) {PropertyName = propertyName, Name = revitParamElement.Name};
            }
            
            var categories = binding.Binding.GetCategories().Select(item => item.Id).ToArray();
            var element = new FilteredElementCollector(document)
                .WherePasses(new ElementMulticategoryFilter(categories))
                .FirstElement();
            
            if(element == null) {
                return new SharedParam(StorageType.None) {PropertyName = propertyName, Name = revitParamElement.Name};
            }

            var param = element.GetParameters(revitParamElement.Name)
                .Where(item => item.IsShared)
                .FirstOrDefault();

            if(param == null) {
                return new SharedParam(StorageType.None) {PropertyName = propertyName, Name = revitParamElement.Name};
            }
            
            return new SharedParam(param.StorageType) {PropertyName = propertyName, Name = revitParamElement.Name};
        }

        /// <inheritdoc />
        public new IEnumerable<SharedParam> GetRevitParams() {
            return base.GetRevitParams().OfType<SharedParam>();
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
            return File.Exists(configPath) ? JsonConvert.DeserializeObject<SharedParamsConfig>(File.ReadAllText(configPath)) : GetDefaultConfig();
        }

        /// <summary>
        /// Возвращает конфигурацию по умолчанию.
        /// </summary>
        /// <returns>Возвращает конфигурацию по умолчанию.</returns>
        public static SharedParamsConfig GetDefaultConfig() {
            return new SharedParamsConfig();
        }
    }
}

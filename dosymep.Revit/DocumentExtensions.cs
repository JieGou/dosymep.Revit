﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dosymep.Revit.Transmissions;

namespace dosymep.Revit {
    /// <summary>
    /// Расширения для документа Revit.
    /// </summary>
    public static class DocumentExtensions {
        #region UnloadLinks

        /// <summary>
        /// Выгружает все связанные файлы для переданных документов Revit.
        /// </summary>
        /// <param name="rvtFileNames">Документы Revit.</param>
        /// <remarks>Работает без использования API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinks(params string[] rvtFileNames) {
            UnloadAllLinks((IEnumerable<string>) rvtFileNames);
        }

        /// <summary>
        /// Выгружает все связанные файлы для переданных документов Revit.
        /// </summary>
        /// <param name="rvtFileNames">Документы Revit.</param>
        /// <remarks>Работает без использования API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinks(IEnumerable<string> rvtFileNames) {
            foreach(string rvtFileName in rvtFileNames) {
                UnloadAllLinks(rvtFileName);
            }
        }

        /// <summary>
        /// Выгружает все связанные файлы для переданного документа Revit.
        /// </summary>
        /// <param name="rvtFileName">Документ Revit.</param>
        /// <remarks>Работает без использования API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinks(string rvtFileName) {
            if(string.IsNullOrEmpty(rvtFileName)) {
                throw new ArgumentException($"'{nameof(rvtFileName)}' cannot be null or empty.", nameof(rvtFileName));
            }

            if(!File.Exists(rvtFileName)) {
                throw new FileNotFoundException("Не был найден файл.", rvtFileName);
            }

            TransmissionData transData = TransmissionData.ReadTransmissionData(rvtFileName);
            transData.IsTransmitted = true;

            foreach(ExternalFileReference externalReference in transData.ExternalFileReferences
                .Where(item => item.ExternalFileReferenceType == ExternalFileReferenceType.CADLink || item.ExternalFileReferenceType == ExternalFileReferenceType.RevitLink)) {
                externalReference.DesiredLoadState = LoadState.Unloaded;
                externalReference.LastSavedLoadState = LoadState.Unloaded;

                externalReference.DesiredPathType = PathType.Relative;
                externalReference.LastSavedPathType = PathType.Relative;

                externalReference.DesiredPath = Path.GetFileName(externalReference.DesiredPath);
                externalReference.LastSavedPath = Path.GetFileName(externalReference.LastSavedPath);
                externalReference.LastSavedCentralServerLocation = null;

                externalReference.LastSavedAbsolutePath = Path.Combine(Path.GetDirectoryName(rvtFileName), Path.GetFileName(externalReference.LastSavedAbsolutePath));
            }

            TransmissionData.WriteTransmissionData(rvtFileName, transData);
        }

        #endregion

        #region  UnloadLinksApi

        /// <summary>
        /// Выгружает все связанные файлы для переданных документов Revit.
        /// </summary>
        /// <param name="rvtFileNames">Документы Revit.</param>
        /// <remarks>Работает c использованием API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinksApi(params string[] rvtFileNames) {
            UnloadAllLinksApi((IEnumerable<string>) rvtFileNames);
        }

        /// <summary>
        /// Выгружает все связанные файлы для переданных документов Revit.
        /// </summary>
        /// <param name="rvtFileNames">Документы Revit.</param>
        /// <remarks>Работает c использованием API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinksApi(IEnumerable<string> rvtFileNames) {
            foreach(string rvtFileName in rvtFileNames) {
                UnloadAllLinksApi(rvtFileName);
            }
        }

        /// <summary>
        /// Выгружает все связанные файлы для переданного документа Revit.
        /// </summary>
        /// <param name="rvtFileName">Документ Revit.</param>
        /// <remarks>Работает c использованием API <see cref="Autodesk.Revit.DB.TransmissionData"/></remarks>
        public static void UnloadAllLinksApi(string rvtFileName) {
            if(string.IsNullOrEmpty(rvtFileName)) {
                throw new ArgumentException($"'{nameof(rvtFileName)}' cannot be null or empty.", nameof(rvtFileName));
            }

            if(!File.Exists(rvtFileName)) {
                throw new FileNotFoundException("Не был найден файл.", rvtFileName);
            }

            Autodesk.Revit.DB.ModelPath rvtModelPath = Autodesk.Revit.DB.ModelPathUtils.ConvertUserVisiblePathToModelPath(rvtFileName);

            Autodesk.Revit.DB.TransmissionData transData = Autodesk.Revit.DB.TransmissionData.ReadTransmissionData(rvtModelPath);
            transData.IsTransmitted = true;

            IEnumerable<Autodesk.Revit.DB.ExternalFileReference> externalReferences = transData.GetAllExternalFileReferenceIds()
                .Select(item => transData.GetLastSavedReferenceData(item))
                .Where(item => item.ExternalFileReferenceType == Autodesk.Revit.DB.ExternalFileReferenceType.CADLink || item.ExternalFileReferenceType == Autodesk.Revit.DB.ExternalFileReferenceType.RevitLink);

            foreach(Autodesk.Revit.DB.ExternalFileReference externalReference in externalReferences) {
                transData.SetDesiredReferenceData(externalReference.GetReferencingId(), externalReference.GetPath(), externalReference.PathType, false);
            }

            Autodesk.Revit.DB.TransmissionData.WriteTransmissionData(rvtModelPath, transData);
        }

        #endregion

        #region IsExistsParam

        /// <summary>
        /// Проверяет на наличие параметра документа.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="paramName">Наименование параметра.</param>
        /// <returns>Возвращает true если параметр был добавлен в Revit, иначе false.</returns>
        /// <exception cref="System.ArgumentException">Наименование параметра не может быть null или пустым.</exception>
        public static bool IsExistsParam(this Autodesk.Revit.DB.Document document, string paramName) {
            if(string.IsNullOrEmpty(paramName)) {
                throw new ArgumentException($"'{nameof(paramName)}' cannot be null or empty.", nameof(paramName));
            }

            return document.GetProjectParamElements().Any(item => item.Name.Equals(paramName));
        }

        /// <summary>
        /// Проверяет на наличие параметра проекта.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="paramName">Наименование параметра проекта.</param>
        /// <returns>Возвращает true если параметр проекта был добавлен в Revit, иначе false.</returns>
        /// <exception cref="System.ArgumentException">Наименование параметра проекта не может быть null или пустым.</exception>
        public static bool IsExistsProjectParam(this Autodesk.Revit.DB.Document document, string paramName) {
            if(string.IsNullOrEmpty(paramName)) {
                throw new ArgumentException($"'{nameof(paramName)}' cannot be null or empty.", nameof(paramName));
            }

            return document.GetProjectParams().Any(item => item.Name.Equals(paramName));
        }

        /// <summary>
        /// Проверяет на наличие общего параметра проекта.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="paramName">Наименование общего параметра проекта.</param>
        /// <returns>Возвращает true если общий параметр проекта был добавлен в Revit, иначе false.</returns>
        /// <exception cref="System.ArgumentException">Наименование общего параметра не может быть null или пустым.</exception>
        public static bool IsExistsSharedParam(this Autodesk.Revit.DB.Document document, string paramName) {
            if(string.IsNullOrEmpty(paramName)) {
                throw new ArgumentException($"'{nameof(paramName)}' cannot be null or empty.", nameof(paramName));
            }

            return document.GetSharedParams().Any(item => item.Name.Equals(paramName));
        }

        #endregion

        #region GetProjectParams

        /// <summary>
        /// Возвращает список параметров проекта.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <returns>Возвращает список параметров проекта.</returns>
        public static IEnumerable<Autodesk.Revit.DB.ParameterElement> GetProjectParams(this Autodesk.Revit.DB.Document document) {
            return document.GetProjectParamElements().Where(item => !(item is Autodesk.Revit.DB.SharedParameterElement));
        }

        /// <summary>
        /// Возвращает список общих параметров.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <returns>Возвращает список общих параметров.</returns>
        public static IEnumerable<Autodesk.Revit.DB.SharedParameterElement> GetSharedParams(this Autodesk.Revit.DB.Document document) {
            return document.GetProjectParamElements().OfType<Autodesk.Revit.DB.SharedParameterElement>();
        }

        /// <summary>
        /// Возвращает все параметры проекта.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <returns>Возвращает все параметры проекта.</returns>
        public static IEnumerable<Autodesk.Revit.DB.ParameterElement> GetProjectParamElements(this Autodesk.Revit.DB.Document document) {
            return document.GetParameterBindings().Select(item => document.GetElement(((dynamic) item.Definition).Id)).OfType<Autodesk.Revit.DB.ParameterElement>();
        }

        /// <summary>
        /// Возвращает связку определения параметра с привязкой.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <returns>Возвращает связку определения параметра с привязкой.</returns>
        public static IEnumerable<(Autodesk.Revit.DB.Definition Definition, Autodesk.Revit.DB.Binding Binding)> GetParameterBindings(this Autodesk.Revit.DB.Document document) {
            var iterator = document.ParameterBindings.ForwardIterator();
            while(iterator.MoveNext()) {
                yield return (iterator.Key, (Autodesk.Revit.DB.Binding) iterator.Current);
            }
        }

        /// <summary>
        /// Возвращает настройки привязки для общего параметра.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="paramName">Наименование параметра.</param>
        /// <returns>Возвращает настройки привязки для общего параметра. Если параметр не был найден возвращает null.</returns>
        public static (Autodesk.Revit.DB.Definition Definition, Autodesk.Revit.DB.Binding Binding) GetSharedParamBinding(
            this Autodesk.Revit.DB.Document document, string paramName) {
            return document.GetParameterBindings()
                .Where(item => item.Definition.Name.Equals(paramName))
                .FirstOrDefault(item => document.IsSharedParamDefinition(item.Definition));
        }
        
        /// <summary>
        /// Возвращает настройки привязки для параметра проекта.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="paramName">Наименование параметра.</param>
        /// <returns>Возвращает настройки привязки для параметра проекта. Если параметр не был найден возвращает null.</returns>
        public static (Autodesk.Revit.DB.Definition Definition, Autodesk.Revit.DB.Binding Binding) GetProjectParamBinding(
            this Autodesk.Revit.DB.Document document,string paramName) {
            return document.GetParameterBindings()
                .Where(item => item.Definition.Name.Equals(paramName))
                .FirstOrDefault(item => document.IsProjectParamDefinition(item.Definition));
        }

        #endregion

        #region ParamDefinition

        /// <summary>
        /// Проверяет является ли определение параметра внутренним.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="definition">Определение параметра.</param>
        /// <returns>Возвращает true - если определение параметра является внутренним, иначе false.</returns>
        public static bool IsSystemParamDefinition(this Autodesk.Revit.DB.Document document, Autodesk.Revit.DB.Definition definition) {
            if(document is null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(definition is null) {
                throw new ArgumentNullException(nameof(definition));
            }

            var builtinParam = GetInternalDefinition(definition)?.BuiltInParameter;
            return builtinParam != null && builtinParam != Autodesk.Revit.DB.BuiltInParameter.INVALID;
        }

        /// <summary>
        /// Проверяет является ли определение параметра общим.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="definition">Определение параметра.</param>
        /// <returns>Возвращает true - если определение параметра является общим, иначе false.</returns>
        public static bool IsSharedParamDefinition(this Autodesk.Revit.DB.Document document, Autodesk.Revit.DB.Definition definition) {
            if(document is null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(definition is null) {
                throw new ArgumentNullException(nameof(definition));
            }

            var elementId = GetInternalDefinition(definition)?.Id ?? Autodesk.Revit.DB.ElementId.InvalidElementId;
            return document.GetElement(elementId) is Autodesk.Revit.DB.SharedParameterElement;
        }

        /// <summary>
        /// Проверяет является ли определение параметра проекта.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="definition">Определение параметра.</param>
        /// <returns>Возвращает true - если определение параметра является проекта, иначе false.</returns>
        public static bool IsProjectParamDefinition(this Autodesk.Revit.DB.Document document, Autodesk.Revit.DB.Definition definition) {
            if(document is null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(definition is null) {
                throw new ArgumentNullException(nameof(definition));
            }

            var elementId = GetInternalDefinition(definition)?.Id ?? Autodesk.Revit.DB.ElementId.InvalidElementId;
            return document.GetElement(elementId) is Autodesk.Revit.DB.ParameterElement
                && !document.IsSystemParamDefinition(definition)
                && !document.IsSharedParamDefinition(definition);
        }

        private static Autodesk.Revit.DB.InternalDefinition GetInternalDefinition(Autodesk.Revit.DB.Definition definition) {
            return definition as Autodesk.Revit.DB.InternalDefinition;
        }

        #endregion

        /// <summary>
        /// Запускает транзакцию на изменение документа.
        /// </summary>
        /// <param name="document">Документ Revit.</param>
        /// <param name="transactionName">Название транзакции.</param>
        /// <returns>Возвращает запущенную транзакцию на изменение документа.</returns>
        public static Autodesk.Revit.DB.Transaction StartTransaction(this Autodesk.Revit.DB.Document document, string transactionName) {
            var transaction = new Autodesk.Revit.DB.Transaction(document);
            transaction.BIMStart(transactionName);

            return transaction;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using dosymep.Bim4Everyone.ProjectParams;
using dosymep.Bim4Everyone.SharedParams;
using dosymep.Revit;

namespace dosymep.Bim4Everyone {
    /// <summary>
    /// Класс расширения элемента
    /// </summary>
    public static class ElementExtensions {
        #region SharedParam

        /// <summary>
        /// Возвращает значение параметра либо значение по умолчанию.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <param name="default">Значение по умолчанию.</param>
        /// <returns>Возвращает значение параметра либо значение по умолчанию.</returns>
        public static object GetParamValueOrDefault(this Element element, SharedParam sharedParam, object @default = default) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            try {
                return element.GetParamValue(sharedParam) ?? @default;
            } catch(ArgumentException) {
                return @default;
            }
        }

        /// <summary>
        /// Возвращает значение параметра элемента.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <returns>Возвращает значение параметра элемента.</returns>
        public static object GetParamValue(this Element element, SharedParam sharedParam) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            return element.GetParam(sharedParam).AsObject();
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, SharedParam sharedParam, double paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            element.GetParam(sharedParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, SharedParam sharedParam, int paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            element.GetParam(sharedParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, SharedParam sharedParam, string paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            element.GetParam(sharedParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, SharedParam sharedParam, ElementId paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            element.GetParam(sharedParam).Set(paramValue);
        }

        /// <summary>
        /// Возвращает параметр.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="sharedParam">Общий параметр.</param>
        /// <returns>Возвращает параметр.</returns>
        public static Parameter GetParam(this Element element, SharedParam sharedParam) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(sharedParam is null) {
                throw new ArgumentNullException(nameof(sharedParam));
            }

            var param = element.GetParameters(sharedParam.Name).FirstOrDefault(item => item.IsShared);
            if(param is null) {
                throw new ArgumentException($"Общий параметр с заданным именем \"{sharedParam.Name}\" не был найден.", nameof(sharedParam));
            }

            if(param.StorageType != sharedParam.SharedParamType) {
                throw new ArgumentException($"Переданный Параметр проекта \"{sharedParam.Name}\" не соответствует типу параметра у элемента.", nameof(sharedParam));
            }

            return param;
        }

        #endregion

        #region ProjectParam

        /// <summary>
        /// Возвращает значение параметра либо значение по умолчанию.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <param name="default">Значение по умолчанию.</param>
        /// <returns>Возвращает значение параметра либо значение по умолчанию.</returns>
        public static object GetParamValueOrDefault(this Element element, ProjectParam projectParam, object @default = default) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            try {
                return element.GetParamValue(projectParam) ?? @default;
            } catch(ArgumentException) {
                return @default;
            }
        }

        /// <summary>
        /// Возвращает значение параметра элемента.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <returns>Возвращает значение параметра элемента.</returns>
        public static object GetParamValue(this Element element, ProjectParam projectParam) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            return element.GetParam(projectParam).AsObject();
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, ProjectParam projectParam, double paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            element.GetParam(projectParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, ProjectParam projectParam, int paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            element.GetParam(projectParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, ProjectParam projectParam, string paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            element.GetParam(projectParam).Set(paramValue);
        }

        /// <summary>
        /// Устанавливает значение параметра.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <param name="paramValue">Значение общего параметра.</param>
        public static void SetParamValue(this Element element, ProjectParam projectParam, ElementId paramValue) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            element.GetParam(projectParam.Name).Set(paramValue);
        }

        /// <summary>
        /// Возвращает параметр.
        /// </summary>
        /// <param name="element">Элемент.</param>
        /// <param name="projectParam">Параметр проекта.</param>
        /// <returns>Возвращает параметр.</returns>
        public static Parameter GetParam(this Element element, ProjectParam projectParam) {
            if(element is null) {
                throw new ArgumentNullException(nameof(element));
            }

            if(projectParam is null) {
                throw new ArgumentNullException(nameof(projectParam));
            }

            var param = element.GetParameters(projectParam.Name).FirstOrDefault(item => !item.IsShared);
            if(param is null) {
                throw new ArgumentException($"Параметр проекта с заданным именем \"{projectParam.Name}\" не был найден.", nameof(projectParam));
            }

            if(param.StorageType != projectParam.SharedParamType) {
                throw new ArgumentException($"Переданный Параметр проекта \"{projectParam.Name}\" не соответствует типу параметра у элемента.", nameof(projectParam));
            }

            return param;
        }

        #endregion
    }
}
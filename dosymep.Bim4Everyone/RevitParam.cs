﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone {
    /// <summary>
    /// Абстрактный класс параметра Revit.
    /// </summary>
    public abstract class RevitParam {
        /// <summary>
        /// Наименование параметра.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Наименование свойства параметра.
        /// </summary>
        [JsonIgnore]
        public virtual string PropertyName { get; internal set; }

        /// <summary>
        /// Описание параметра.
        /// </summary>
        [JsonIgnore]
        public virtual string Description { get; }

        /// <summary>
        /// Тип параметра.
        /// </summary>
        [JsonIgnore]
        public virtual StorageType SharedParamType { get; }
    }
}
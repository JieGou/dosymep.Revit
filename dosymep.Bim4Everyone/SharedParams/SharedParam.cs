﻿using System.Collections.Generic;

using Autodesk.Revit.DB;

using pyRevitLabs.Json;

namespace dosymep.Bim4Everyone.SharedParams {
    public class SharedParam {
        private readonly static Dictionary<string, string> _description = new Dictionary<string, string>() {
            { nameof(SharedParamsConfig.SizeDepth), "Описание SizeDepth" },
            { nameof(SharedParamsConfig.SizeWidth), "Описание SizeWidth" },
            { nameof(SharedParamsConfig.BulkheadClass), "Описание BulkheadClass" },
            { nameof(SharedParamsConfig.BulkheadDepth), "Описание BulkheadDepth" },
            { nameof(SharedParamsConfig.BulkheadExists), "Описание BulkheadExists" },
            { nameof(SharedParamsConfig.BulkheadLength), "Описание BulkheadLength" },
        };

        private readonly static Dictionary<string, StorageType> _sharedParamTypes = new Dictionary<string, StorageType>() {
            { nameof(SharedParamsConfig.SizeDepth), StorageType.Double },
            { nameof(SharedParamsConfig.SizeWidth), StorageType.Double },
            { nameof(SharedParamsConfig.BulkheadClass), StorageType.String },
            { nameof(SharedParamsConfig.BulkheadDepth), StorageType.String },
            { nameof(SharedParamsConfig.BulkheadExists), StorageType.String },
            { nameof(SharedParamsConfig.BulkheadLength), StorageType.String },
        };

        internal SharedParam() { }

        public string Name { get; set; }

        [JsonIgnore]
        public string Description {
            get { return _description.TryGetValue(PropertyName, out string value) ? value : null; }
        }

        [JsonIgnore]
        public string PropertyName { get; internal set; }

        [JsonIgnore]
        public StorageType SharedParamType {
            get { return _sharedParamTypes.TryGetValue(PropertyName, out StorageType value) ? value : StorageType.None; }
        }
    }
}

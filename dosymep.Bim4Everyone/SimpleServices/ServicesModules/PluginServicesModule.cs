﻿using System;
using System.Linq;

using Ninject;
using Ninject.Modules;
using Ninject.Planning.Bindings;

namespace dosymep.Bim4Everyone.SimpleServices.ServicesModules {
    /// <summary>
    /// Абстрактный класс модуля для плагина.
    /// </summary>
    public abstract class PluginServicesModule : NinjectModule {
        /// <summary>
        /// Инициализация модуля плагина для контейнера зависимостей.
        /// </summary>
        public void InitModule() {
            ServicesProvider.Instance.Load(this);
        }

        /// <inheritdoc />
        public override void Unload() {
            base.Unload();
            if(Kernel != null) {
                foreach(IBinding binding in Bindings) {
                    Kernel.Unbind(binding.Service);
                }

                Bindings.Clear();
            }
        }

        /// <inheritdoc />
        public override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if(disposing) {
                Kernel?.Unload(Name);
            }
        }
    }
}
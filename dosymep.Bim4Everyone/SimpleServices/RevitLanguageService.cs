using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;

using dosymep.SimpleServices;

namespace dosymep.Bim4Everyone.SimpleServices {

    /// <summary>
    /// Предоставляет доступ к языку Revit.
    /// </summary>
    public class RevitLanguageService : ILanguageService {
        private readonly UIApplication _uiApp;

        /// <summary>
        /// Инициализирует сервис для получения доступа к языку Revit.
        /// </summary>
        /// <param name="uiApp">Текущая сессия пользовательсокого интерфейса Revit.</param>
        public RevitLanguageService(UIApplication uiApp) {
            _uiApp = uiApp;
        }

        /// <inheritdoc/>
        public Language HostLanguage => GetLanguage();

        private Language GetLanguage() {
            switch(_uiApp.Application.Language) {
                case Autodesk.Revit.ApplicationServices.LanguageType.Unknown:
                return Language.EnglishUSA;
                case Autodesk.Revit.ApplicationServices.LanguageType.English_USA:
                return Language.EnglishUSA;
                case Autodesk.Revit.ApplicationServices.LanguageType.German:
                return Language.German;
                case Autodesk.Revit.ApplicationServices.LanguageType.Spanish:
                return Language.Spanish;
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                return Language.French;
                case Autodesk.Revit.ApplicationServices.LanguageType.Italian:
                return Language.Italian;
                case Autodesk.Revit.ApplicationServices.LanguageType.Dutch:
                return Language.Dutch;
                case Autodesk.Revit.ApplicationServices.LanguageType.Chinese_Simplified:
                return Language.ChineseSimplified;
                case Autodesk.Revit.ApplicationServices.LanguageType.Chinese_Traditional:
                return Language.ChineseTraditional;
                case Autodesk.Revit.ApplicationServices.LanguageType.Japanese:
                return Language.Japanese;
                case Autodesk.Revit.ApplicationServices.LanguageType.Korean:
                return Language.Korean;
                case Autodesk.Revit.ApplicationServices.LanguageType.Russian:
                return Language.Russian;
                case Autodesk.Revit.ApplicationServices.LanguageType.Czech:
                return Language.Czech;
                case Autodesk.Revit.ApplicationServices.LanguageType.Polish:
                return Language.Polish;
                case Autodesk.Revit.ApplicationServices.LanguageType.Hungarian:
                return Language.Hungarian;
                case Autodesk.Revit.ApplicationServices.LanguageType.Brazilian_Portuguese:
                return Language.BrazilianPortuguese;
                case Autodesk.Revit.ApplicationServices.LanguageType.English_GB:
                return Language.EnglishGB;
                default:
                throw new ArgumentOutOfRangeException(nameof(_uiApp.Application.Language));
            }
        }

    }
}

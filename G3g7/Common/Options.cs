using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace G3g7.Common {
    public class Options {
        private const string HideValueKey = "HideValue";
        private const string MonochromeKey = "Monochrome";
        private const string CosmosKey = "Cosmos";
        private readonly ISyncLocalStorageService localStorage;
        private readonly NavigationManager navigationManager;
        private bool isHideValue;
        private bool isMonochrome;
        private string cosmos = "Man";
        public Options(ISyncLocalStorageService localStorage, NavigationManager navigationManager, string inititalCosmos = null) {
            this.localStorage = localStorage;
            this.navigationManager = navigationManager;
            IsHideValue = localStorage.GetItem<bool>(HideValueKey);
            IsMonochrome = localStorage.GetItem<bool>(MonochromeKey);

            if (inititalCosmos is null) {
                Cosmos = localStorage.GetItem<string>(CosmosKey);
            }
            else {
                Cosmos = inititalCosmos;
            }
        }

        internal bool IsHideValue {
            get => isHideValue;
            set {
                if (isHideValue != value) {
                    isHideValue = value;
                    localStorage.SetItem(HideValueKey, isHideValue);
                }
            }
        }

        internal bool IsMonochrome {
            get => isMonochrome;
            set {
                if (isMonochrome != value) {
                    isMonochrome = value;
                    localStorage.SetItem(MonochromeKey, isMonochrome);
                }
            }
        }

        internal string Cosmos {
            get => cosmos;
            set {
                if (cosmos != value && Cosmoses.Contains(value)) {
                    cosmos = value;
                    localStorage.SetItem(CosmosKey, cosmos);
                    navigationManager.NavigateTo($"{cosmos}");
                }
            }
        }

        internal static IEnumerable<string> Cosmoses { get; } = new string[] {
            "One",
            "Holy",
            "Galaxy",
            "Sun",
            "Planet",
            "Man",
            "Atom"
        };
    }
}

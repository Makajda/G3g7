using Blazored.LocalStorage;
using System.Collections.Generic;

namespace G3g7.Common {
    public class Options {
        private const string HideValueKey = "HideValue";
        private const string MonochromeKey = "Monochrome";
        private const string CosmosKey = "Cosmos";
        private readonly ISyncLocalStorageService localStorage;
        private bool isHideValue;
        private bool isMonochrome;
        private int cosmos;
        public Options(ISyncLocalStorageService localStorage) {
            this.localStorage = localStorage;
            IsHideValue = localStorage.GetItem<bool>(HideValueKey);
            IsMonochrome = localStorage.GetItem<bool>(MonochromeKey);
            Cosmos = localStorage.GetItem<int>(CosmosKey);
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

        internal int Cosmos {
            get => cosmos;
            set {
                if (cosmos != value) {
                    cosmos = value;
                    localStorage.SetItem(CosmosKey, cosmos);
                }
            }
        }

        internal IEnumerable<string> Cosmoses { get; } = new string[] {
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

using Blazored.LocalStorage;

namespace G3g7.Common {
    public class Storage {
        private const string FirstRunKey = "FirstRun";
        private readonly ISyncLocalStorageService localStorage;
        private readonly Options options;
        public Storage(ISyncLocalStorageService localStorage, Options options) {
            this.localStorage = localStorage;
            this.options = options;
            if (!localStorage.ContainKey(FirstRunKey)) {
                SetDefault();
            }
        }

        internal void Clear() {
            localStorage.Clear();
            SetDefault();
        }

        internal bool GetVisible(string id) {
            var key = VisibleKey(id);
            return !localStorage.ContainKey(key) || localStorage.GetItem<bool>(key);
        }

        internal string GetLegend(string id) => localStorage.GetItem<string>(LegendKey(id));

        internal void SetVisible(string id, bool isVisible) {
            if (isVisible) {
                localStorage.RemoveItem(VisibleKey(id));
            }
            else {
                localStorage.SetItem(VisibleKey(id), isVisible);
            }
        }

        internal void SetLegend(string id, string legend) {
            if (legend is null) {
                localStorage.RemoveItem(LegendKey(id));
            }
            else {
                localStorage.SetItem(LegendKey(id), legend);
            }
        }

        private static string VisibleKey(string id) => $"{id}-V";
        private string LegendKey(string id) => $"{options.Cosmos}-{id}-L";

        private void SetDefault() {
            localStorage.SetItem(FirstRunKey, false);

            localStorage.SetItem("One-0-L", "One");
            localStorage.SetItem("Holy-0-L", "Holy");
            localStorage.SetItem("Galaxy-0-L", "Galaxy");
            localStorage.SetItem("Sun-0-L", "Sun");
            localStorage.SetItem("Planet-0-L", "Planet");
            localStorage.SetItem("Man-0-L", "Man");
            localStorage.SetItem("Atom-0-L", "Atom");

            localStorage.SetItem("One-0-1-L", "Sun");
            localStorage.SetItem("One-0-2-L", "Planet");
            localStorage.SetItem("One-0-2-1-L", "Moon");
            localStorage.SetItem("One-0-2-2-L", "Anulios");

            localStorage.SetItem("Atom-0-L", "Atom");
            localStorage.SetItem("Atom-0-1-L", "Nucleus");
            localStorage.SetItem("Atom-0-2-L", "Electrons");
            localStorage.SetItem("Atom-0-1-1-L", "Protons");
            localStorage.SetItem("Atom-0-1-2-L", "Neutrons");

            localStorage.SetItem("Man-0-1-L", "Higher Centers");
            localStorage.SetItem("Man-0-1-1-L", "Higher Mental Center");
            localStorage.SetItem("Man-0-1-2-L", "Higher Emotional Center");
            localStorage.SetItem("Man-0-2-L", "Physical Body");
            localStorage.SetItem("Man-0-2-1-L", "Astral Body");
            localStorage.SetItem("Man-0-2-1-1-L", "Mental Center");
            localStorage.SetItem("Man-0-2-1-2-L", "Emotional Center");
            localStorage.SetItem("Man-0-2-2-L", "Planetary Body");
            localStorage.SetItem("Man-0-2-2-1-L", "Movement Center");
            localStorage.SetItem("Man-0-2-2-2-L", "Instinctive Center");
        }
    }
}

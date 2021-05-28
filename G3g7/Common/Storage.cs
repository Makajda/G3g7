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
                localStorage.SetItem(FirstRunKey, false);
                SetDefault();
            }
        }

        internal bool GetVisible(string id) {
            var key = VisibleKey(id);
            return !localStorage.ContainKey(key) || localStorage.GetItem<bool>(key);
        }

        internal string GetLegend(string id) {
            return localStorage.GetItem<string>(LegendKey(id));
        }

        internal void SetVisible(string id, bool isVisible) {
            localStorage.SetItem(VisibleKey(id), isVisible);
        }

        internal void SetLegend(string id, string legend) {
            localStorage.SetItem(LegendKey(id), legend);
        }

        private static string VisibleKey(string id) => $"{id}-V";
        private string LegendKey(string id) => $"{options.Cosmos}-{id}-L";

        private void SetDefault() {
            // cosmos-[octave]-V
            localStorage.SetItem("0-0-1-V", "One");
        }
    }
}

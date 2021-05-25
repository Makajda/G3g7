using Blazored.LocalStorage;

namespace G3g7.Common {
    public class Storage {
        private readonly ISyncLocalStorageService localStorage;
        private readonly Options options;
        public Storage(ISyncLocalStorageService localStorage, Options options) {
            this.localStorage = localStorage;
            this.options = options;
        }

        internal bool GetVisible(string id) {
            return !localStorage.ContainKey(VisibleKey(id)) || localStorage.GetItem<bool>(VisibleKey(id));
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

        private string VisibleKey(string id) => $"{id}-V";
        private string LegendKey(string id) => $"{options.Cosmos}-{id}-L";
    }
}

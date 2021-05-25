using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace G3g7.Common {
    public class Octave {
        private const int IdOne = 1;
        private const int IdTwo = 2;
        private readonly ISyncLocalStorageService localStorage;
        private double begin;
        private double end;
        private bool isVisible;
        private string legend;
        public Octave(ISyncLocalStorageService localStorage) {
            this.localStorage = localStorage;
            this.begin = 24576d;
            this.end = 0;
            Id = "0";
            IsVisible = localStorage.ContainKey(VisibleKey) ? localStorage.GetItem<bool>(VisibleKey) : true;
            Legend = localStorage.GetItem<string>(LegendKey);
        }
        public Octave(Octave parent, int id) {
            localStorage = parent.localStorage;
            Parent = parent;
            Id = parent is null ? "0" : $"{parent.Id}-{id}";
            IsVisible = localStorage.GetItem<bool>(VisibleKey);
            Legend = localStorage.GetItem<string>(LegendKey);
            if (id == IdOne) {
                begin = parent.So;
                end = parent.Mi;
            }
            else {
                begin = parent.Mi;
                end = parent.end;
            }
        }

        public bool IsVisible {
            get => isVisible;
            set {
                if (isVisible != value) {
                    isVisible = value;
                    localStorage.SetItem(VisibleKey, IsVisible);
                }
            }
        }

        public string Legend {
            get => legend;
            set {
                if (legend != value) {
                    legend = value;
                    localStorage.SetItem(LegendKey, Legend);
                }
            }
        }

        public Octave NextOne { get; set; }
        public Octave NextTwo { get; set; }
        public string Id { get; }
        public Octave Parent { get; }

        public double Do => begin;
        public double Ti => (7 * begin + end) / 8d;
        public double La => (2 * begin + end) / 3d;
        public double So => (begin + end) / 2d;
        public double Fa => (begin + 2 * end) / 3d;
        public double Mi => (begin + 3 * end) / 4d;
        public double Re => (begin + 7 * end) / 8d;

        public static async Task CreateNext(Octave octave) {
            if (octave.NextOne is null) {
                octave.NextOne = new Octave(octave, IdOne);
                octave.NextTwo = new Octave(octave, IdTwo);
            }
            else {
                await CreateNext(octave.NextOne);
                await CreateNext(octave.NextTwo);
            }
        }

        public static async Task Recalc(Octave octave, double k) {
            octave.Recalc(k);
            if (octave.NextOne is not null) {
                await Recalc(octave.NextOne, k);
                await Recalc(octave.NextTwo, k);
            }
        }

        public void Recalc(double k) {
            begin *= k;
            end *= k;
        }

        private string VisibleKey => $"{Id}-V";
        private string LegendKey => $"{Id}-L";
    }
}

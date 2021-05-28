using System.Threading.Tasks;

namespace G3g7.Common {
    internal class Octave {
        private const int IdOne = 1;
        private const int IdTwo = 2;
        private static Octave root;
        private static Storage storage;
        private readonly string id;
        private double begin;
        private double end;
        private bool isVisible;
        private string legend;
        public Octave(Storage storage) {
            Octave.storage = storage;
            begin = 24576d;
            end = 0;
            root = this;
            id = "0";
            IsVisible = storage.GetVisible(id);
            Legend = storage.GetLegend(id);
        }

        private Octave(Octave parent, int suffix) {
            Parent = parent;
            id = parent is null ? "0" : $"{parent.id}-{suffix}";
            IsVisible = storage.GetVisible(id);
            Legend = storage.GetLegend(id);
            if (suffix == IdOne) {
                begin = parent.So;
                end = parent.Mi;
            }
            else {
                begin = parent.Mi;
                end = parent.end;
            }
        }

        internal bool IsVisible {
            get => isVisible;
            set {
                if (isVisible != value) {
                    isVisible = value;
                    storage.SetVisible(id, IsVisible);
                }
            }
        }

        internal string Legend {
            get => legend;
            set {
                if (legend != value) {
                    legend = value;
                    storage.SetLegend(id, Legend);
                }
            }
        }

        internal Octave NextOne { get; set; }
        internal Octave NextTwo { get; set; }
        internal Octave Parent { get; }

        internal double Do => begin;
        internal double Ti => (7 * begin + end) / 8d;
        internal double La => (2 * begin + end) / 3d;
        internal double So => (begin + end) / 2d;
        internal double Fa => (begin + 2 * end) / 3d;
        internal double Mi => (begin + 3 * end) / 4d;
        internal double Re => (begin + 7 * end) / 8d;

        internal static async Task Recalc(double k) => await Recalc(root, k);
        internal static async Task CreateNext() => await CreateNext(root);
        internal static async Task ChangeCosmos() => await ChangeCosmos(root);

        private static async Task Recalc(Octave octave, double k) {
            octave.begin *= k;
            octave.end *= k;
            if (octave.NextOne is not null) {
                await Recalc(octave.NextOne, k);
                await Recalc(octave.NextTwo, k);
            }
        }

        private static async Task CreateNext(Octave octave) {
            if (octave.NextOne is null) {
                octave.NextOne = new Octave(octave, IdOne);
                octave.NextTwo = new Octave(octave, IdTwo);
            }
            else {
                await CreateNext(octave.NextOne);
                await CreateNext(octave.NextTwo);
            }
        }

        private static async Task ChangeCosmos(Octave octave) {
            octave.IsVisible = storage.GetVisible(octave.id);
            octave.Legend = storage.GetLegend(octave.id);
            if (octave.NextOne is not null) {
                await ChangeCosmos(octave.NextOne);
                await ChangeCosmos(octave.NextTwo);
            }
        }
    }
}

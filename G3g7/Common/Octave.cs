using System.Threading.Tasks;

namespace G3g7.Common {
    public class Octave {
        private double begin;
        private double end;
        public Octave(double begin, double end) {
            this.begin = begin;
            this.end = end;
        }

        public double Do => begin;
        public double Ti => (7 * begin + end) / 8d;
        public double La => (2 * begin + end) / 3d;
        public double So => (begin + end) / 2d;
        public double Fa => (begin + 2 * end) / 3d;
        public double Mi => (begin + 3 * end) / 4d;
        public double Re => (begin + 7 * end) / 8d;

        public string Legend { get; set; }

        public Octave NextOne { get; set; }
        public Octave NextTwo { get; set; }

        public async Task CreateNext(Octave octave) {
            if (octave.NextOne is null) {
                octave.NextOne = new Octave(octave.So, octave.Mi);
                octave.NextTwo = new Octave(octave.Mi, octave.end);
            }
            else {
                await CreateNext(octave.NextOne);
                await CreateNext(octave.NextTwo);
            }
        }

        public async Task Recalc(Octave octave, double k) {
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
    }
}

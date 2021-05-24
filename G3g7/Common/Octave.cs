using System;
using System.Threading.Tasks;

namespace G3g7.Common {
    public class Octave {
        private double begin;
        private double end;
        public Octave(double begin, double end, Octave parent = null) {
            this.begin = begin;
            this.end = end;
            Parent = parent;
        }

        public string Legend { get; set; }
        public Octave NextOne { get; set; }
        public Octave NextTwo { get; set; }
        public bool IsVisible { get; set; } = true;
        public Octave Parent { get; set; }

        public double Do => begin;
        public double Ti => (7 * begin + end) / 8d;
        public double La => (2 * begin + end) / 3d;
        public double So => (begin + end) / 2d;
        public double Fa => (begin + 2 * end) / 3d;
        public double Mi => (begin + 3 * end) / 4d;
        public double Re => (begin + 7 * end) / 8d;

        public static async Task CreateNext(Octave octave) {
            if (octave.NextOne is null) {
                octave.NextOne = new Octave(octave.So, octave.Mi, octave);
                octave.NextTwo = new Octave(octave.Mi, octave.end, octave);
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

        public void SetLegends(string legends) {

        }

        public string GetLegends() {
            var legends = Legend;
            return legends;
        }
    }
}

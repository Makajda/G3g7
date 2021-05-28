using System.Threading.Tasks;

namespace G3g7.Common {
    public class World {
        internal int Level { get; set; }
        internal bool IsNext { get; set; }
        internal World Next { get; set; }

        internal async Task CreateWorld() {
            if (IsNext) {
                IsNext = false;
            }
            else {
                if (Next is null) {
                    Next = new() { Level = Level + 1 };
                    await Octave.CreateNext();
                }

                IsNext = true;
            }
        }
    }
}

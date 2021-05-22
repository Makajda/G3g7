using System.Collections.Generic;

namespace G3g7.Common {
    public class Options {
        public bool IsHideValue { get; set; }
        public bool IsMonochrome { get; set; }
        public int Cosmos { get; set; }
        public IEnumerable<string> Cosmoses { get; } = new string[] {
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

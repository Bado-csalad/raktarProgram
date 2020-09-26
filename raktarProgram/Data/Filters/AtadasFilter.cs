using System;
using System.Collections.Generic;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class AtadasFilter
    {
        public string Kereses { get; set; }
        public DateTime? Mettol { get; set; }
        public DateTime? Meddig { get; set; }

        public List<Tuple<string, string>> Sorrend { get; set; }
        public List<Tuple<string, bool>> MibeKeres { get; set; }
        public List<Tuple<string, string>> RegiSorrend { get; set; }

        public bool MaIs { get; set; }
        public bool CsakMa { get; set; }

        public int? Mennyiseg { get; set; }
    }
}
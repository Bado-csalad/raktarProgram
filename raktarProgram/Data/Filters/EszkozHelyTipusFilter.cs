using System;
using System.Collections.Generic;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class EszkozHelyTipusFilter
    {
        public string Kereses { get; set; }
        public bool? Aktive { get; set; }

        public List<Tuple<string, string>> Sorrend { get; set; }
        public List<Tuple<string, bool>> MibeKeres { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class EszkozFilter
    {
        public string Kereses { get; set; }
        public bool? Aktive { get; set; }

        public List<Tuple<string, string>> Sorrend { get; set; }
        public List<Tuple<string, bool>> MibeKeres { get; set; }
    }
}
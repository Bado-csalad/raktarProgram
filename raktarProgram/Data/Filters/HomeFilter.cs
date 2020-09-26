using System;
using System.Collections.Generic;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class HomeFilter
    {
        public List<Tuple<string, string>> Sorrend { get; set; }
        public List<Tuple<string, bool>> MibeKeres { get; set; }
        public string Kereses { get; set; }
    }
}
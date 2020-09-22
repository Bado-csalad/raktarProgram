using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class EszkozTipusFilter
    {
        public string Kereses { get; set; }

        public List<Tuple<string, string>> Sorrend { get; set; }
        public List<Tuple<string, bool>> MibeKeres { get; set; }

    }
}

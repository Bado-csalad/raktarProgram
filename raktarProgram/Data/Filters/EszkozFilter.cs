using System;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class EszkozFilter
    {
        public string Kereses { get; set; }
        public bool? Aktive { get; set; }
    }
}
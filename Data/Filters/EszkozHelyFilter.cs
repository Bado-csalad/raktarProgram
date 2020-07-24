using System;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class EszkozHelyFilter
    {
        public string Kereses { get; set; }
        public bool? Aktive { get; set; }
    }
}
using System;

namespace raktarProgram.Data.Filters
{
    [Serializable]
    public class AtadasFilter
    {
        public string Kereses { get; set; }
        public DateTime? Mettol { get; set; }
        public DateTime? Meddig { get; set; }
    }
}
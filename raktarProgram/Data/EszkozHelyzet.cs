using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Data
{
    public class EszkozHelyzet
    {
        public int ID { get; set; }
        public string RovidNev { get; set; }
        public string Leiras { get; set; }
        public ICollection<Eszkoz> Eszkozok { get; set; }
    }
}

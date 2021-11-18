using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace raktarProgram.Data
{
    public class EszkozTipus
    {
        public int ID { get; set; }
        public string Nev { get; set;  }
        public bool? Torolt { get; set; }
        public string Leiras { get; set; }
        public int? UseTimes { get; set; }
        public ICollection<Eszkoz> Eszkozok { get; set; }
    }
}

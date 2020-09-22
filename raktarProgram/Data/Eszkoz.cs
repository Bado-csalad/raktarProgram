using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace raktarProgram.Data
{
    public class Eszkoz
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public bool? Torolt { get; set; }
        public string Azonosito { get; set; }
        public bool? Aktiv { get; set; }
        [ForeignKey("EszkozTipusID")]
        public EszkozTipus Tipus { get; set; }
        public int EszkozTipusID { get; set; }
        public ICollection<Hely> Helyek { get; set; }
        [NotMapped]
        public int HelyDarab { get; set; }

    }
}
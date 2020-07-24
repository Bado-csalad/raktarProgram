using System;
using System.Collections.Generic;

namespace raktarProgram.Data
{
    public class Hely
    {

        public const string Ki = "KI";
        public const string Be = "BE";

        public int ID { get; set; }
        public int FelhasznaloID { get; set; }
        public int Mennyiseg { get; set; }
        public int EszkozID { get; set; }
        public int Kodegyutt { get; set; }
        public DateTime Mikortol { get; set; }
        public DateTime? Meddig { get; set; }
        public string Megjegyzes { get; set; }
        public int EszkozHelyID { get; set; }
        public Felhasznalo Felhasznalo { get; set; }
        public Eszkoz Eszkoz { get; set; }
        public EszkozHely EszkozHely { get; set; }

        public string Irany { get; set; }
    }
}
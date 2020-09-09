using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? HovaID { get; set; }
        public Felhasznalo Felhasznalo { get; set; }
        [ForeignKey("EszkozID")]
        public Eszkoz Eszkoz { get; set; }
        [ForeignKey("EszkozHelyID")]
        [InverseProperty("Helyek")]
        public EszkozHely EszkozHely { get; set; }
        [ForeignKey("HovaID")]
        [InverseProperty("HelyekHova")]
        public EszkozHely? Hova { get; set; }
        public int? HovaMennyiseg { get; set; }

        public string Irany { get; set; }

        public string EszkozhelyNev {
            get{
                return this.EszkozHely?.Nev;
            }
        }
    }
}
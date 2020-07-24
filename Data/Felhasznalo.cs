using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace raktarProgram.Data
{
    public class Felhasznalo
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs email cím megadva")]
        public string Email { get; set; }
        public string Telefon { get; set; }
        public bool? Belephet { get; set; }
        public bool? Torolt { get; set; }
        public ICollection<Hely> Helyek { get; set; }
    }
}
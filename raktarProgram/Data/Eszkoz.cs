using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace raktarProgram.Data
{
    public class Eszkoz
    {
        public int ID { get; set; }
        
        [Display(Name = "Eszköz neve", Prompt = "Add meg az eszköz nevét", Description="Eszköz neve")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="Nincs megadva")]
        [Required(ErrorMessage = "Az eszköz nevét kötelező megadni!")]
        public string Nev { get; set; }
        [Display(Name = "Eszköz leírása", Prompt = "Add meg az eszköz leírását", Description="Eszköz leírása")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="Nincs megadva")]
        public string Leiras { get; set; }

        [Display(Name = "Törölt e az eszköz", Prompt = "Törölt e", Description="Törölve van e")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="")]
        [Required(ErrorMessage = "Kötelező megadni hogy törölt e!")]
        public bool? Torolt { get; set; }
        
        [Display(Name = "Eszköz azonosítója", Prompt = "Add meg az eszköz azonosítóját", Description="Eszköz azonosítója")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="Nincs megadva")]
        public string Azonosito { get; set; }
        [Display(Name = "Aktiv e az eszköz", Prompt = "Aktív e", Description="Aktív van e")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="")]
        [Required(ErrorMessage = "Kötelező megadni hogy aktív e!")]
        public bool? Aktiv { get; set; }

        public ICollection<Hely> Helyek { get; set; }

    }
}
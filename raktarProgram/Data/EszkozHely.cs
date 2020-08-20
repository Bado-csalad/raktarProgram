using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace raktarProgram.Data
{
    public class EszkozHely
    {
        public int ID { get; set; }

        [Display(Name = "Név", Prompt = "Add meg az nevét", Description = "Tárolási hely neve")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs megadva tárolási hely")]
        [Required(ErrorMessage = "Az tárolási hely nevét kötelező megadni!")]
        public string Nev { get; set; }

        [Display(Name = "Leírás", Prompt = "Add meg az leírását", Description = "Leírás")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs megadva leírás")]
        public string Leiras { get; set; }

        [Display(Name = "Telefonszám", Prompt = "Add meg a telefonszámát", Description = "Telefonszám")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs megadva telefonszám")]
        public string Telefon { get; set; }

        [Display(Name = "Cím", Prompt = "Add meg az címét", Description = "Címe")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs megadva cím")]
        public string Cim { get; set; }

        [Display(Name = "Email", Prompt = "Add meg az email-címét", Description = "Email cím")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nincs megadva email cím")]
        public string Email { get; set; }

        [Display(Name = "Eszközhely Tipusa", Prompt = "Milyen a tipusa", Description = "Típus")]
        [Required(ErrorMessage = "Kötelező megadni az eszközhely tipusát")]
        public int TipusID { get; set; }

        [Display(Name = "Nincs többé", Prompt = "Megszünt?", Description = "nincs")]
        [Required(ErrorMessage = "Kötelező megadni hogy törölt e!")]
        public bool? Torolt { get; set; }
        
        [Display(Name = "Használatban van még?", Prompt = "Használatba lesz állíatva", Description = "használva van e vagy nincs")]
        [Required(ErrorMessage = "Kötelező megadni hogy használatba van e vagy nincs!")]
        public bool? aktiv { get; set; }

        public ICollection<Hely> Helyek { get; set; }
        
        [ForeignKey("TipusID")]
        public EszkozHelyTipus Tipus { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace raktarProgram.Data
{
    public class EszkozHelyTipus
    {
        public int ID { get; set; }

        [Display(Name = "Név", Prompt = "Add meg az nevét", Description="Tárolási hely tipusának neve")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="Nincs megadva tárolási hely tipus")]
        [Required(ErrorMessage = "Az tárolási hely tipusának nevét kötelező megadni!")]
        public string Nev { get; set; }
        [Display(Name = "Leírás", Prompt = "Add meg az learását", Description="Tárolási hely tipusának leírása")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="Nincs megadva leírás")]
        public string Leiras { get; set; }
        [Display(Name = "Nincs többé", Prompt = "Megszünt?", Description="nincs")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="")]
        [Required(ErrorMessage = "Kötelező megadni hogy törölt e!")]
        public bool? Torolt { get; set; }
        [Display(Name = "Használatban van még?", Prompt = "Használatba lesz állíatva", Description="használva van e vagy nincs")]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText="")]
        [Required(ErrorMessage = "Kötelező megadni hogy használatba van e vagy nincs!")]

        public bool? Aktiv { get; set; }
        public bool? LehetNegativ { get; set; }
        public bool? NullaListabanLathato { get; set; }
        
        public ICollection<EszkozHely> EszkozHelyek { get; set; }
    }
}
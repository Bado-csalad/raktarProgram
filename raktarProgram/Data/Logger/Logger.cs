using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Data.Logger
{
    public enum Level
    {
        Verbose,
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }
    public class Logger
    {
        [DisallowNull]
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string Message { get; set; }
        [AllowNull]
        public string MessageTemplate { get; set; }
        [AllowNull] 
        public Level Level { get; set; }
        [DisallowNull]
        public DateTime TimeStamp { get; set; }
        [AllowNull]
        public string Exception { get; set; }
        [AllowNull]
        public string Properties { get; set; }

    }
}

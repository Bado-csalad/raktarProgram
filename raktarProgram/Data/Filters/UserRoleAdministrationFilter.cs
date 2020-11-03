using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Data.Filters
{
    public class UserRoleAdministrationFilter
    {
        public string Kereses { get; set; }

        public List<Tuple<string, string>> Sorrend { get; set; }

        public bool Admin { get; set; }
        public bool Leader { get; set; }
        public bool Visitor { get; set; }

    }
}

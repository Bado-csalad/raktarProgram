using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Repositories
{
    public static class FelhasznaloInitializer
    {
        public static void Initialize(FelhasznaloDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine("Okeés");

        }
    }
}

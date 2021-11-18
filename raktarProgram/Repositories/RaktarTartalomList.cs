using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Interfaces;

namespace raktarProgram.Repositories
{
    public class RaktarTartalomList : IRaktarTartalomList
    {
        private RaktarContext context;

        public RaktarTartalomList(RaktarContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Eszkoz> Eszkoz => context.Eszkoz.Include(x => x.Helyzet);
        public IQueryable<EszkozHelyzet> HelyzetList => context.EszkozHelyzet;

        public async Task<ListResult<Eszkoz>> ListEszkozHelyzet(string roviditett, int pageSize, int pageNum)
        {
            var lista = this.Eszkoz.Where(x => x.Torolt == false);

            lista = lista.Where(x => x.Helyzet.RovidNev == roviditett);


            ListResult<Eszkoz> res = new ListResult<Eszkoz>();

            res.Total = await lista.CountAsync();

            res.Data = await lista
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            res.Data.ForEach(c =>
            {
                c.HelyDarab = this.context.Hely.Count(d => d.EszkozID == c.ID);
            });

            return res;

        }
    }
}

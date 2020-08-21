using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;
using System.Threading.Tasks;
using raktarProgram.Helpers;

namespace raktarProgram.Repositories
{
    public class EszkozHelyTipusRepository : IEszkozHelyTipusRepository
    {
        private RaktarContext context;

        public EszkozHelyTipusRepository(RaktarContext ctx)
        {
            context = ctx;
        }

        public IQueryable<EszkozHelyTipus> EszkozHelyTipus => context.EszkozHelyTipus;

        public async Task<EszkozHelyTipus> EszkozHelyTipusFelvetel(EszkozHelyTipus data)
        {
            context.Add(data);
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<EszkozHelyTipus> EszkozHelyTipusModositas(EszkozHelyTipus eszkozHelyTipus)
        {
            context.Attach(eszkozHelyTipus);
            context.Update(eszkozHelyTipus);

            await context.SaveChangesAsync();
            return eszkozHelyTipus;

        }

        public async Task<EszkozHelyTipus> EszkozHelyTipusTorles(int ID)
        {
            var e = context.EszkozHelyTipus.Where(c => c.ID == ID).First();
            e.Torolt = true;
            context.Update(e);
            await context.SaveChangesAsync();
            return e;
        }

        public async Task<ListResult<EszkozHelyTipus>> ListEszkozHelyTipus(EszkozHelyTipusFilter filter, int pageSize, int pageNum)
        {
            var lista = this.EszkozHelyTipus.Where(x => x.Torolt == false && x.LehetNegativ == false);

            if (filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Nev.Contains(filter.Kereses)
                            || x.Leiras.Contains(filter.Kereses));
                }

                if(filter.Aktive.HasValue)
                {
                    lista = lista.Where(x => x.Aktiv == filter.Aktive.Value);
                }
            }


            ListResult<EszkozHelyTipus> res = new ListResult<EszkozHelyTipus>();

            res.Total = await lista.CountAsync();

            res.Data = await lista.OrderBy(x => x.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            return res;
        }
    }
}
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
    public class EszkozHelyRepository : IEszkozHelyRepository
    {
        private RaktarContext context;
        public EszkozHelyRepository(RaktarContext ctx)
        {
            context = ctx;
        }

        public IQueryable<EszkozHely> EszkozHely => context.EszkozHely.Include(x => x.Tipus);
        public IQueryable<EszkozHelyTipus> EszkozHelyTipus => context.EszkozHelyTipus;

        public async Task<EszkozHely> EszkozHelyFelvetel(EszkozHely eszkozHely)
        {
            eszkozHely.Tipus = this.context.EszkozHelyTipus.First(c => c.ID == eszkozHely.TipusID);
            eszkozHely.Torolt = false;
            eszkozHely.aktiv = true;
            context.Add(eszkozHely);
            await context.SaveChangesAsync();
            return eszkozHely;
        }

        public async Task<EszkozHely> EszkozHelyModositas(EszkozHely eszkozHely)
        {
            // context.Entry(eszkozHely).State = EntityState.Modified;
            context.Attach(eszkozHely);

            context.Update(eszkozHely);

            await context.SaveChangesAsync();
            return eszkozHely;
        }

        public async Task<EszkozHely> EszkozHelyTorles(int ID)
        {
            var e = context.EszkozHely.Where(c => c.ID == ID).First();
            e.Torolt = true;
            context.Update(e);
            await context.SaveChangesAsync();
            return e;
        }

        public async Task<ListResult<EszkozHely>> ListEszkozHely(EszkozHelyFilter filter, int pageSize, int pageNum)
        {
            var lista = this.EszkozHely.Where(x => x.Torolt == false && x.Tipus.LehetNegativ == false);

            if(filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Leiras.Contains(filter.Kereses)
                            || x.Nev.Contains(filter.Kereses)
                            || x.Telefon.Contains(filter.Kereses)
                            || x.Tipus.Nev.Contains(filter.Kereses)
                            || x.Cim.Contains(filter.Kereses)
                            || x.Email.Contains(filter.Kereses));
                }

                if(filter.Aktive.HasValue)
                {
                    lista = lista.Where(x => x.aktiv.Value);
                }
            }

            ListResult<EszkozHely> res = new ListResult<EszkozHely>();

            res.Total = await lista.CountAsync();

            res.Data = await lista.OrderBy(x => x.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .Include(t=> t.Tipus)
                        .ToListAsync();
            
            return res;
        }
    }
}
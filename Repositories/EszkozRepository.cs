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
    public class EszkozRepository : IEszkozRepository
    {
        private RaktarContext context;
        public EszkozRepository(RaktarContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Eszkoz> Eszkoz => context.Eszkoz;

        public async Task<Eszkoz> EszkozFelvetel(Eszkoz data)
        {
            context.Add(data);
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<Eszkoz> EszkozModositas(Eszkoz eszkoz)
        {
            context.Entry(eszkoz).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return eszkoz;
        }      

        public async Task<Eszkoz> EszkozTorles(int ID)
        {
            var e = await context.Eszkoz.Where(c => c.ID == ID).FirstAsync();
            e.Torolt = true;
            context.Update(e);
            await context.SaveChangesAsync();
            return e;
        }
        
        public async Task<ListResult<Eszkoz>> ListEszkoz(EszkozFilter filter, int pageSize,  int pageNum)
        {
            var lista = this.Eszkoz.Where(x => x.Torolt == false);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Azonosito.Contains(filter.Kereses) 
                            || x.Nev.Contains(filter.Kereses)
                            || x.Leiras.Contains(filter.Kereses));
                }

                if (filter.Aktive.HasValue)
                {
                    lista = lista.Where(c => c.Aktiv == filter.Aktive.Value);
                }
            }

            ListResult<Eszkoz> res = new ListResult<Eszkoz>();
            
            res.Total = await lista.CountAsync();

            res.Data = await lista.OrderBy(x => x.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .ToArrayAsync();

            return res;
        }
    }

}
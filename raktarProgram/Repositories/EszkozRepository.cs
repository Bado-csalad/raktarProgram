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
        public IQueryable<Eszkoz> Eszkoz => context.Eszkoz.Include(x => x.Tipus);
        public IQueryable<EszkozTipus> EszkozTipus => context.EszkozTipus;

        public async Task<Eszkoz> EszkozFelvetel(Eszkoz data)
        {
            data.Aktiv = true;
            data.Torolt = false;
            context.Add(data);
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<Eszkoz> EszkozModositas(Eszkoz eszkoz)
        {
            // context.Entry(eszkoz).State = EntityState.Modified;
            context.Attach(eszkoz);
            context.Update(eszkoz);

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

        public async Task<int> GetHelyCount(Eszkoz eszkoz)
        {
            return await this.context.Hely.CountAsync(c => c.EszkozID == eszkoz.ID);
        }

        public async Task<ListResult<Eszkoz>> ListEszkoz(EszkozFilter filter, int pageSize, int pageNum)
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

                if (filter.Sorrend != null && filter.Sorrend.Count > 0)
                {
                    foreach (var c in filter.Sorrend)
                    {
                        if (c.Item1 == "Nev")
                        {
                            if (c.Item2 == "A")
                            {
                                lista = lista.OrderBy(c => c.Nev);
                            }
                            else
                            {
                                lista = lista.OrderByDescending(c => c.Nev);
                            }
                        }

                        if (c.Item1 == "Azonosito")
                        {
                            if (c.Item2 == "A")
                            {
                                lista = lista.OrderBy(c => c.Azonosito);
                            }
                            else
                            {
                                lista = lista.OrderByDescending(c => c.Azonosito);
                            }
                        }

                        if (c.Item1 == "Leiras")
                        {
                            if (c.Item2 == "A")
                            {
                                lista = lista.OrderBy(c => c.Leiras);
                            }
                            else
                            {
                                lista = lista.OrderByDescending(c => c.Leiras);
                            }
                        }
                    }
                }
            }

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
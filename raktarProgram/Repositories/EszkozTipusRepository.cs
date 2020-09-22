using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;
using System.Threading.Tasks;
using raktarProgram.Helpers;
using System.Runtime.CompilerServices;

namespace raktarProgram.Repositories
{
    public class EszkozTipusRepository : IEszkozTipusRepository
    {
        private RaktarContext context;
        public EszkozTipusRepository(RaktarContext ctx)
        {
            context = ctx;
        }
        public IQueryable<EszkozTipus> EszkozTipus => context.EszkozTipus;

        public async Task<EszkozTipus> EszkozTipusFelvetel(EszkozTipus data)
        {
            data.Torolt = false;
            context.Add(data);
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<EszkozTipus> EszkozTipusModositas(EszkozTipus eszkozTipus)
        {
            context.Attach(eszkozTipus);
            context.Update(eszkozTipus);

            await context.SaveChangesAsync();
            return eszkozTipus;
        }

        public async Task<EszkozTipus> EszkozTipusTorles(int ID)
        {
            var e = await context.EszkozTipus.Where(c => c.ID == ID).FirstAsync();
            e.Torolt = true;
            context.Update(e);
            await context.SaveChangesAsync();
            return e;
        }

        public async Task<ListResult<EszkozTipus>> ListEszkozTipus(EszkozTipusFilter filter, int pageSize, int pageNum)
        {
            var lista = this.EszkozTipus;

            if(filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Nev.Contains(filter.Kereses)
                                        || x.Leiras.Contains(filter.Kereses));
                }

                if(filter.Sorrend != null && filter.Sorrend.Count > 0)
                {
                    foreach(var c in filter.Sorrend)
                    {
                        if(c.Item1 == "Nev")
                        {
                            if (c.Item2 == "A")
                            {
                                lista = lista.OrderBy(x => x.Nev);
                            }
                            else
                            {
                                lista = lista.OrderByDescending(x => x.Nev);
                            }
                        }

                        if (c.Item1 == "Leiras")
                        {
                            if (c.Item2 == "A")
                            {
                                lista = lista.OrderBy(x => x.Leiras);
                            }
                            else
                            {
                                lista = lista.OrderByDescending(x => x.Leiras);
                            }
                        }
                    }
                }
            }


            ListResult<EszkozTipus> res = new ListResult<EszkozTipus>();

            res.Total = await lista.CountAsync();

            res.Data = await lista
                           .Skip((pageNum - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();

            return res;
        }
    }
}

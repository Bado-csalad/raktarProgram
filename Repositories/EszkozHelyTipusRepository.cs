using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;

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

        public void EszkozHelyTipusFelvetel(EszkozHelyTipus data)
        {
            context.Add(data);
            context.SaveChanges();
        }

        public void EszkozHelyTipusModositas(EszkozHelyTipus eszkozHelyTipus)
        {
            var e = context.EszkozHelyTipus.Where(c => c.ID == eszkozHelyTipus.ID).First();

            e.Nev = eszkozHelyTipus.Nev;
            e.Leiras = eszkozHelyTipus.Leiras;
            e.Torolt = eszkozHelyTipus.Torolt;
            e.Aktiv = eszkozHelyTipus.Aktiv;

            context.Update(e);
            context.SaveChanges();

        }

        public void EszkozHelyTipusTorles(int ID)
        {
            var e = context.EszkozHelyTipus.Where(c => c.ID == ID).First();
            e.Torolt = true;
            context.Update(e);
            context.SaveChanges();
        }

        public IQueryable<EszkozHelyTipus> ListEszkozHelyTipus(EszkozHelyTipusFilter filter, int pageSize, int pageNum, out int totalCount)
        {
            var lista = this.EszkozHelyTipus.Where(x => x.Torolt == false);

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

            totalCount = lista.Count();

            lista = lista.OrderBy(x => x.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize);

            return lista;
        }
    }
}
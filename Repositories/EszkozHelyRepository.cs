using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;

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

        public void EszkozHelyFelvetel(EszkozHely eszkozHely)
        {
            eszkozHely.Tipus = this.context.EszkozHelyTipus.First(c => c.ID == eszkozHely.TipusID);
            context.Add(eszkozHely);
            context.SaveChanges();
        }

        public void EszkozHelyModositas(EszkozHely eszkozHely)
        {
            var e = context.EszkozHely.Where(c => c.ID == eszkozHely.ID).First();

            e.Nev = eszkozHely.Nev;
            e.Leiras = eszkozHely.Leiras;
            e.Telefon = eszkozHely.Telefon;
            e.Cim = eszkozHely.Cim;
            e.Email = eszkozHely.Email;
            e.Torolt = eszkozHely.Torolt;
            e.aktiv = eszkozHely.aktiv;
            e.Tipus = this.context.EszkozHelyTipus.First(c => c.ID == eszkozHely.TipusID);

            context.Update(e);
            context.SaveChanges();
        }

        public void EszkozHelyTorles(int ID)
        {
            var e = context.EszkozHely.Where(c => c.ID == ID).First();
            e.Torolt = true;
            context.Update(e);
            context.SaveChanges();
        }

        public IQueryable<EszkozHely> ListEszkozHely(EszkozHelyFilter filter, int pageSize, int pageNum, out int totalCount)
        {
            var lista = this.EszkozHely.Where(x => x.Torolt == false);

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

            totalCount = lista.Count();

            lista = lista.OrderBy(x => x.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize);
            
            return lista;
        }
    }
}
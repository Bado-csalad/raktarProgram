using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;

namespace raktarProgram.Repositories
{
    public class HomeRespitory : IHomeRespitory
    {
        private RaktarContext context;
        public HomeRespitory(RaktarContext ctx)
        {
            context = ctx;
        }

        public IQueryable<EszkozHely> EszkozHely => context.EszkozHely.Include(x => x.Tipus);
        public IQueryable<EszkozHelyTipus> EszkozHelyTipus => context.EszkozHelyTipus;
        public IQueryable<Eszkoz> Eszkoz => context.Eszkoz;
        public IQueryable<Felhasznalo> Felhasznalo => context.Felhasznalo;
        public IQueryable<Hely> Hely => context.Hely.Include(x => x.Eszkoz).Include(x => x.EszkozHely).Include(x => x.Felhasznalo);


        public IQueryable<Hely> ListHome(HomeFilter filter, int pageSize, int pageNum, out int totalCount, out IQueryable<Eszkoz> eszkozLista, out IQueryable<EszkozHely> eszkozHelyLista)
        {
            var lista = this.Hely.Where(x => x.Meddig == null && x.EszkozHely.Tipus.LehetNegativ == false);
            if( filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Eszkoz.Nev.Contains(filter.Kereses) 
                            || x.EszkozHely.Nev.Contains(filter.Kereses));
                }
            }
            

            totalCount = lista.Count();

            lista = lista.OrderBy(x => x.EszkozHely.Nev)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize);

            eszkozLista = this.Eszkoz.Where(x => x.Helyek == lista);
            eszkozHelyLista = this.EszkozHely;

            return lista;
        }
    }
}
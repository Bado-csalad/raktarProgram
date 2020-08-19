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

        private Params Param => context.Params.First();

        public async Task<ListResult<Hely>> ListHome(HomeFilter filter, int pageSize, int pageNum)
        {
            var lista = this.Hely; //.Where(x => x.Meddig == null && x.EszkozHely.Tipus.LehetNegativ == false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Eszkoz.Nev.Contains(filter.Kereses)
                            || x.EszkozHely.Nev.Contains(filter.Kereses));
                }
            }

            ListResult<Hely> res = new ListResult<Hely>();

            res.Total = await lista.CountAsync();

            res.Data = await lista.OrderBy(x => x.Mikortol)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .Include(t => t.Eszkoz)
                        .Include(t => t.EszkozHely)
                        .ToListAsync();

            return res;
        }

        public async Task<List<Eszkoz>> GetXMitList()
        {
            var lista = await this.Hely.Where(c => c.Meddig == null && c.Mennyiseg > 0)
            .Select(t => t.Eszkoz)
            .Distinct()
            .OrderBy(c => c.Nev)
            .ToListAsync();

            return lista;
        }

        public async Task<List<Hely>> GetXKitolList(int eszkozID)
        {
            var lista = await this.Hely
                .Where(c => c.EszkozID == eszkozID && c.Meddig == null && c.Mennyiseg > 0 && c.EszkozHely.Tipus.LehetNegativ == false)
                .OrderBy(c => c.EszkozHely.Nev)
                .Include(t => t.EszkozHely)
                .ToListAsync();

            return lista;
        }

        public async Task<List<EszkozHely>> GetXHovaList(int eszkohelyID)
        {
            var lista = await this.EszkozHely
                .Where(c => c.ID != eszkohelyID && c.Torolt == false && c.Tipus.LehetNegativ == false)
                .OrderBy(c => c.Nev)
                .ToListAsync();

            return lista;
        }

        // cucc kiadása
        /*
        UI:
        xmit: ESZKOZ.NEV választása (azok jelenjenek meg, ahol hely.meddig = null és hely.menny > 0)
        xkitől: HELY.ESZKOZHELY.NEV választása (azok jelenjenek meg, ahol eszköz = kiválasztott és hely.meddig = null és hely.menny > 0 és nem lehet beszerzés)
        xennyiség: darab megadása, legfeljebb annyi lehet, amennyi a kitől (hely.menny) sorban van
        xhova: ESZKOZHELY választása (azok jelenjenek meg, ami nem egyezik meg a kitől-lel és nem lehet beszerzés)
        xmikor: dátum megadása
        xmegj: szöveg megadása

        mentés:
        kodegy meghatározása (this.Param)
        a xkitől.update sor meddig = xmikor
        xkitől mentés
        ujdarab = kitől.menny-xmennyiség
        ha ujdarab < 0, hibaüzenet: túl sokat akarsz elvenni - mentés vége
        új hely felvétele
            mennnyiseg = ujdarab
            eszkoz = xmit
            mikortol = xmikor
            meddig = null
            megjegyzes = xmegj
            ezskozhely = xkitől
            irany = KI
            Kodegyutt = this.Param.Kodeggyutt
            új hely mentése
        hova: hely keresése, ahol eszkoz = xmit és ezskozhely = xhova és meddig = null
        ha hova létezik
            hova.update sor meddig = xmikor
            ujdarab_hova = hova.menny
            hova mentése
        ujdarab_hova += xmennyiség
        új hely felvétele
            mennnyiseg = ujdarab
            eszkoz = xmit
            mikortol = xmikor
            meddig = null
            megjegyzes = xmegj
            ezskozhely = xhova
            irany = BE
            Kodegyutt = this.Param.Kodeggyutt
            új hely mentése
        this.Param.Kodeggyutt++
        Params mentése



        */

        // beszerzés
    }
}
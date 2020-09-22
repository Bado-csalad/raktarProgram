using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;
using System.Threading.Tasks;
using raktarProgram.Helpers;
using System;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Cryptography;

namespace raktarProgram.Repositories
{
    public class HomeRespitory : IHomeRespitory
    {
        public const string nincsXmit = "Válaszd ki, hogy mit szeretnél átadni!";
        public const string nincsXKitol = "Válaszd ki, hogy ki adja át a tételt!";
        public const string nincsXHova = "Válaszd ki, hogy hova szeretnéd átadni a tételt!";
        public const string rosszMennyiseg = "Nincs ennyi tárgy a ratkárban";
        public const string sikeresFelvetel = "Eszköz sikeresen átadva";
        public const string kevesMennyiseg = "Túl kevés mennyiséget adtál meg";
        public const string nincsXmitBesz = "Válaszd ki, hogy mit szeretnél beszerezni!";

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

        public async Task<string> HelyModositas(Hely hely)
        {
            var tarsHely = await this.Hely.Where(c => c.Kodegyutt == hely.Kodegyutt && c.ID != hely.ID).SingleAsync();
            var regiHely = await this.Hely.Where(c => c.ID == hely.ID).SingleAsync();

            if (hely.Mennyiseg <= 0)
            {
                return (kevesMennyiseg);
            }

            tarsHely.Mennyiseg = -hely.Mennyiseg;
            tarsHely.Eszkoz = hely.Eszkoz;
            tarsHely.Mikortol = hely.Mikortol;
            tarsHely.Meddig = hely.Mikortol;

            context.Update(tarsHely);
            context.Update(hely);

            await context.SaveChangesAsync();
            return null;
        }

        public async Task<ListResult<Hely>> ListHome(HomeFilter filter, int pageSize, int pageNum)
        {
            var lista = this.Hely.Where(x => x.Meddig == null && x.EszkozHely.Tipus.LehetNegativ == false && x.Mennyiseg > 0);
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

        public async Task<ListResult<Hely>> ListAtadasok(AtadasFilter filter, int pageSize, int pageNum)
        {

            var lista = this.Hely
                .Join(this.Hely,
                p => p.Kodegyutt,
                o => o.Kodegyutt,
                (p, e) => new { honnan = p, hova = e })
                   .Where(c => c.honnan.Irany == "KI"
                    && c.honnan.ID != c.hova.ID)
                   .Select(c => c.honnan);

            lista = lista.Where(c => c.EszkozHely.Tipus.LehetNegativ == false);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Eszkoz.Nev.Contains(filter.Kereses)
                            || x.EszkozHely.Nev.Contains(filter.Kereses)
                            || x.Hova.Nev.Contains(filter.Kereses));
                }
                if (filter.Meddig != null)
                {
                    if (filter.Mettol != filter.Meddig)
                    {

                        lista = lista.Where(x => x.Meddig <= filter.Meddig
                                && x.Mikortol <= filter.Meddig);
                    }
                }
                if (filter.Mettol != null)
                {
                    if(filter.Mettol == filter.Meddig)
                    {
                        lista = lista.Where(x => x.Meddig == null
                            && x.Mikortol >= filter.Mettol);
                    }
                    else
                    {
                    lista = lista.Where(x => x.Mikortol >= filter.Mettol);
                    }
                    
                }
            }

            ListResult<Hely> res = new ListResult<Hely>();

            res.Total = await lista.CountAsync();

            lista = lista.OrderBy(x => x.EszkozHely.Nev)
                        .OrderBy(x => x.Eszkoz.Nev)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .Include(t => t.Eszkoz)
                        .Include(t => t.EszkozHely);

            res.Data = await lista
                        .ToListAsync();

            return res;
        }

        public async Task<ListResult<Hely>> ListBeszerzesek(BeszerzesFilter filter, int pageSize, int pageNum)
        {
            var lista = this.Hely
                .Join(this.Hely,
                p => p.Kodegyutt,
                o => o.Kodegyutt,
                (p, e) => new { beszerzes = p, raktar = e })
                   .Where(c => c.beszerzes.EszkozHely.Tipus.LehetNegativ == true
                    && c.raktar.EszkozHely.Tipus.NullaListabanLathato == true)
                   .Select(c => c.raktar);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Kereses))
                {
                    lista = lista.Where(x => x.Eszkoz.Nev.Contains(filter.Kereses)
                            || x.EszkozHely.Nev.Contains(filter.Kereses));
                }
            }

            /* 
             select * from hely as beszerzes
             inner join hely as raktar 
                ON raktar.kodefggyutt = beszerzes.kodeggyutt
              inner join ezskozheky ehb on ehb.id = berszerhes.ezskozhelyID
              inner join eszkozhelytipus etb on etb.id = ehb.tipusid and etb.lehetnegativ = true
            
            inner join ezskozheky ehr on ehr.id = raktar.ezskozhelyID
              inner join eszkozhelytipus etr on etr.id = ehr.tipusid and etr.NullaListabanLathato = true
            
            WHERE 
              ..... 
             */

            ListResult<Hely> res = new ListResult<Hely>();
            lista = lista.Where(x => x.Mennyiseg > 0);
            res.Total = await lista.CountAsync();

            lista = lista.OrderBy(x => x.Mikortol)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .Include(t => t.Eszkoz)
                        .Include(t => t.EszkozHely);

            res.Data = await lista
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

        public async Task<List<EszkozHely>> GetXHovaBeszerzesList()
        {
            var lista = await this.EszkozHely
                .Where(c => c.Tipus.NullaListabanLathato == true && c.Torolt == false)
                .OrderBy(c => c.Nev)
                .ToListAsync();
            return lista;
        }

        public async Task<List<EszkozHely>> GetXHovaAtadasokList()
        {
            var lista = await this.EszkozHely
                .Where(c => c.Tipus.LehetNegativ == false && c.Tipus.Torolt == false)
                .OrderBy(c => c.Nev)
                .ToListAsync();
            return lista;
        }

        public async Task<string> Xbeszerzes(Eszkoz xmit,
                                              EszkozHely xhova, DateTime xmikor,
                                              int xmennyiseg, string xmegj)
        {
            return await this.Xmentes(xmit, this.Hely.First(c => c.EszkozHely.Tipus.LehetNegativ == true), xhova, xmikor, xmennyiseg, xmegj);
        }

        public async Task<string> Xmentes(Eszkoz xmit, Hely xkitol,
                                              EszkozHely xhova, DateTime xmikor,
                                              int xmennyiseg, string xmegj)
        {
            if (xmit == null || xmit.ID == 0)
            {
                if(xkitol != null)
                {
                    return (nincsXmitBesz);
                }
                else
                {
                return (nincsXmit);
                }
            }

            if (xkitol == null || xkitol.ID == 0)
            {
                return (nincsXKitol);
            }

            if (xhova == null || xhova.ID == 0)
            {
                return (nincsXHova);
            }

            if (xmennyiseg <= 0)
            {
                return (kevesMennyiseg);
            }

            int kodegy = this.Param.Kodegyutt;
            var kitol = await this.Hely.Where(c => c.ID == xkitol.ID).SingleAsync();
            kitol.Meddig = xmikor;

            int ujdarab = kitol.Mennyiseg - xmennyiseg;

            if (ujdarab < 0 && kitol.EszkozHely.Tipus != null)
            {
                return (rosszMennyiseg);
            }

            Hely ujhely = new Hely()
            {
                Mennyiseg = ujdarab,
                Eszkoz = xmit,
                Mikortol = xmikor,
                Meddig = null,
                Megjegyzes = xmegj,
                EszkozHely = xkitol.EszkozHely,
                Irany = "KI",
                Kodegyutt = this.Param.Kodegyutt,
                Felhasznalo = context.Felhasznalo.First(),
                Hova = xhova,
                HovaMennyiseg = xmennyiseg
            };

            context.Hely.Add(ujhely);
            await context.SaveChangesAsync();

            var hova = await context.Hely.Where(c => c.EszkozHely.ID == xhova.ID && c.Eszkoz.ID == xmit.ID && c.Meddig == null).SingleOrDefaultAsync();
            int mennyi = xmennyiseg;

            if (hova != null)
            {
                mennyi += hova.Mennyiseg;
                hova.Meddig = xmikor;
                await context.SaveChangesAsync();
            }

            Hely ujHova = new Hely()
            {
                Mennyiseg = mennyi,
                Eszkoz = xmit,
                Mikortol = xmikor,
                Meddig = null,
                Megjegyzes = xmegj,
                EszkozHely = xhova,
                Irany = "BE",
                Kodegyutt = this.Param.Kodegyutt,
                Felhasznalo = context.Felhasznalo.First()
            };

            context.Add(ujHova);
            this.Param.Kodegyutt++;
            await context.SaveChangesAsync();

            return (sikeresFelvetel);

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

        /*
         
         uj lap kinél mennyi ideig volt 
         keresés idő intervallum, mire kinél hol mikor stb...
         
         */ 
    }
}
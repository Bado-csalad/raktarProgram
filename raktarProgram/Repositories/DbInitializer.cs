using System;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using raktarProgram.Data;

namespace raktarProgram.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(RaktarContext context)
        {

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            if (!context.Roles.Any(c => c.Id == "admin"))
            {
                var rolea = new IdentityRole() { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" };
                var rolel = new IdentityRole() { Id = "leader", Name = "Leader", NormalizedName = "LEADER" };
                var rolev = new IdentityRole() { Id = "visitor", Name = "Visitor", NormalizedName = "VISITOR" };

                var hasher = new PasswordHasher<IdentityUser>();

                var user = new IdentityUser
                {
                    Id = "1",
                    Email = "bado.mate@outlook.com",
                    UserName = "bado.mate@outlook.com",
                    EmailConfirmed = true
                };

                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                user.PasswordHash = hasher.HashPassword(user, "temporarypass");

                var iur = new IdentityUserRole<string>
                {
                    RoleId = "admin",
                    UserId = "1"
                };

                using (TransactionScope ts = new TransactionScope())
                {
                    var identityRole = new IdentityRole[]
                    { rolea, rolel, rolev };

                    context.Roles.AddRange(identityRole);
                    context.SaveChanges();

                    context.Users.Add(user);
                    context.SaveChanges();

                    context.UserRoles.Add(iur);
                    context.SaveChanges();

                    ts.Complete();
                }
            }

            if (!context.EszkozHelyTipus.Any())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    Params p = new Params();
                    p.Kodegyutt = 1;
                    context.Params.Add(p);
                    context.SaveChanges();

                    var eh1 = new EszkozHelyTipus { Nev = "raktar", Leiras = "raktarleiras", Torolt = false, Aktiv = true, LehetNegativ = false, NullaListabanLathato = true };
                    var eh2 = new EszkozHelyTipus { Nev = "ember", Leiras = "embernel van", Torolt = false, Aktiv = true, LehetNegativ = false, NullaListabanLathato = false };
                    var eh3 = new EszkozHelyTipus { Nev = "beszerzes", Leiras = "vasarlas", Torolt = false, Aktiv = false, LehetNegativ = true, NullaListabanLathato = false };

                    var eszkozHelyTipus = new EszkozHelyTipus[]
                    { eh1, eh2, eh3 };

                    context.EszkozHelyTipus.AddRange(eszkozHelyTipus);
                    context.SaveChanges();

                    var besz1 = new EszkozHely { Nev = "beszerzes1", Leiras = "ezbeszerzes1", Tipus = eh3, Torolt = false, aktiv = true };

                    var rak1 = new EszkozHely { Nev = "raktar1", Leiras = "ezraktar1", Tipus = eh1, Torolt = false, aktiv = true };
                    var rak2 = new EszkozHely { Nev = "raktar2", Leiras = "ezraktar2", Tipus = eh1, Torolt = false, aktiv = true };
                    var jozsi = new EszkozHely { Nev = "jozsieszh", Leiras = "ezjozsieszh", Tipus = eh2, Torolt = false, aktiv = true };
                    var pista = new EszkozHely { Nev = "pistaeszh", Leiras = "ezpistaeszh", Tipus = eh2, Torolt = false, aktiv = true };

                    var eszkozhely = new EszkozHely[]
                    {
                    rak1,
                    rak2,
                    new EszkozHely{ Nev = "raktar3", Leiras = "ezraktar3", Tipus = eh1, Torolt = false, aktiv = true},
                    new EszkozHely{ Nev = "belaeszh", Leiras = "ezbelaeszh", Tipus = eh2, Torolt = false, aktiv = true},
                    jozsi,
                    pista,
                    besz1
                    };

                    context.EszkozHely.AddRange(eszkozhely);
                    context.SaveChanges();

                    var baltak = new EszkozTipus { Nev = "baltak", Leiras = "ezek baltak" };
                    var satrak = new EszkozTipus { Nev = "satrak", Leiras = "ezek satrak" };
                    var asok = new EszkozTipus { Nev = "asok", Leiras = "ezek asok" };
                    var fureszek = new EszkozTipus { Nev = "fureszek", Leiras = "ezek fureszek" };

                    var tipus = new EszkozTipus[]
                    {
                        baltak,
                        satrak,
                        asok,
                        fureszek
                    };

                    context.EszkozTipus.AddRange(tipus);

                    var eszkoz = new Eszkoz[]
                    {
                    new Eszkoz { Nev = "baltafekete", Leiras = " ez egy fekete balta", Azonosito = "baltfeazon", Torolt = false, Aktiv = true, Tipus = baltak },
                    new Eszkoz { Nev = "baltapiros", Leiras = " ez egy piros balta", Azonosito = "baltpiazon", Torolt = false, Aktiv = true, Tipus = baltak},
                    new Eszkoz { Nev = "baltakek", Leiras = " ez egy kek balta", Azonosito = "baltkeazon", Torolt = false, Aktiv = true, Tipus = baltak},
                    new Eszkoz { Nev = "satorfekete", Leiras = " ez egy fekete sator", Azonosito = "safeazon", Torolt = false, Aktiv = true, Tipus = satrak},
                    new Eszkoz { Nev = "satorpiros", Leiras = " ez egy piros sator", Azonosito = "sapiazon", Torolt = false, Aktiv = true, Tipus = satrak},
                    new Eszkoz { Nev = "satorkek", Leiras = " ez egy kek sator", Azonosito = "sakeazon", Torolt = false, Aktiv = true, Tipus = satrak},
                    new Eszkoz { Nev = "asofekete", Leiras = " ez egy fekete aso", Azonosito = "asfeazon", Torolt = false, Aktiv = true, Tipus = asok},
                    new Eszkoz { Nev = "asopiros", Leiras = " ez egy piros aso", Azonosito = "aspiazon", Torolt = false, Aktiv = true, Tipus = asok},
                    new Eszkoz { Nev = "asokek", Leiras = " ez egy kek aso", Azonosito = "askeazon", Torolt = false, Aktiv = true, Tipus = asok},
                    new Eszkoz { Nev = "fureszfekete", Leiras = " ez egy fekete furesz", Azonosito = "fufeazon", Torolt = false, Aktiv = true, Tipus = fureszek},
                    new Eszkoz { Nev = "fureszpiros", Leiras = " ez egy piros furesz", Azonosito = "fupiazon", Torolt = false, Aktiv = true, Tipus = fureszek},
                    new Eszkoz { Nev = "fureszkek", Leiras = " ez egy kek furesz", Azonosito = "fukeazon", Torolt = false, Aktiv = true, Tipus = fureszek},
                    };

                    context.Eszkoz.AddRange(eszkoz);
                    context.SaveChanges();

                    var felhasznalo = new Felhasznalo[]
                    {
                    new Felhasznalo { Nev = "belafh", Leiras = "béla kinek feje olyan mint egy béka", Email = "bela@gmail.com", Telefon = "+11111", Belephet = true, Torolt = false},
                    new Felhasznalo { Nev = "jozsifh", Leiras = "józsi milyen a kocsi", Email = "jozsi@gmail.com", Telefon = "+22222", Belephet = false, Torolt = true},
                    new Felhasznalo { Nev = "pistafh", Leiras = "pista kinek szaga olyan mint a pisa", Email = "pista@gmail.com", Telefon = "+33333", Belephet = false, Torolt = false},
                    new Felhasznalo { Nev = "erikfh", Leiras = "erik kinek a vérét pálinkából nyerik", Email = "erik@gmail.com", Telefon = "+44444", Belephet = true, Torolt = false},
                    new Felhasznalo { Nev = "alexanderfh", Leiras = "alex mint a rolex", Email = "alex@gmail.com", Telefon = "+55555", Belephet = false, Torolt = true},
                    new Felhasznalo { Nev = "ivofh", Leiras = "ivó, mindnent ivó", Email = "ivo@gmail.com", Telefon = "+66666", Belephet = false, Torolt = true},
                    new Felhasznalo { Nev = "milanfh", Leiras = "milán kinek hátán kijon a villám", Email = "milan@gmail.com", Telefon = "+77777", Belephet = false, Torolt = false},
                    new Felhasznalo { Nev = "bernatfh", Leiras = "bernát felakasztotta magát", Email = "bernat@gmail.com", Telefon = "+88888", Belephet = true, Torolt = false},
                    new Felhasznalo { Nev = "feliciafh", Leiras = "felícia ki kiakasztotta a macskának a karmát", Email = "felicia@gmail.com", Telefon = "+99999", Belephet = false, Torolt = false},
                    new Felhasznalo { Nev = "konstantinfh", Leiras = "konstantin magát ki rabolja ki", Email = "konstantin@gmail.com", Telefon = "+00000", Belephet = true, Torolt = false}
                    };


                    context.Felhasznalo.AddRange(felhasznalo);
                    context.SaveChanges();

                    var felh = context.Felhasznalo.First();


                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltfeazon"),
                            Felhasznalo = felh,
                            EszkozHely = besz1,
                            Mikortol = new DateTime(2020, 01, 23), // mindig kötelező
                            Meddig = new DateTime(2020, 01, 23),
                            Mennyiseg = -1,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "fekete balta beszerzés, raktár1-be  1 tétel",
                            Irany = Hely.Ki
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltfeazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 01, 23),
                            Meddig = null, // ha irany = BE --> nem szabad tölteni Meddig-et, de egy kapcsolódó későbbi kivét beállítja a kivét dátumára
                            Mennyiseg = 1,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "fekete balta beszerzés, raktár1-be 1 tétel",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltpiazon"),
                            Felhasznalo = felh,
                            EszkozHely = besz1,
                            Mikortol = new DateTime(2020, 01, 23),
                            Meddig = new DateTime(2020, 01, 23),
                            Mennyiseg = -2,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "piros balta beszerzés, raktár1-be 2 tétel",
                            Irany = Hely.Ki
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltpiazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 01, 23),
                            Meddig = new DateTime(2020, 03, 05),
                            Mennyiseg = 2,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "piros balta beszerzés, raktár1-be 2 tétel",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = besz1,
                            Mikortol = new DateTime(2020, 01, 23),
                            Meddig = new DateTime(2020, 01, 23),
                            Mennyiseg = -3,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "fekete sátor beszerzés, raktár1-be 3 tétel",
                            Irany = Hely.Ki
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 01, 23),
                            Meddig = new DateTime(2020, 02, 23),
                            Mennyiseg = 3,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "fekete sátor beszerzés, raktár1-be  3 tétel",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 02, 23),
                            Meddig = new DateTime(2020, 04, 12),
                            Mennyiseg = 1,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "pista elvitt a raktar1-ből 2 fekete sátrat",
                            Irany = Hely.Ki,
                            Hova = pista,
                            HovaMennyiseg = 2
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = pista,
                            Mikortol = new DateTime(2020, 02, 23),
                            Meddig = new DateTime(2020, 04, 12),
                            Mennyiseg = 2,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "pista elvitt a raktar1-ből 2 fekete sátrat",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltpiazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 03, 05),
                            Meddig = null,
                            Mennyiseg = 1,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "józsi elvitt a raktar1-ből 1 piros baltát",
                            Irany = Hely.Ki,
                            Hova = jozsi,
                            HovaMennyiseg = 1
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "baltpiazon"),
                            Felhasznalo = felh,
                            EszkozHely = jozsi,
                            Mikortol = new DateTime(2020, 03, 05),
                            Meddig = null,
                            Mennyiseg = 1,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "józsi elvitt a raktar1-ből 1 piros baltát",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = pista,
                            Mikortol = new DateTime(2020, 04, 12),
                            Meddig = null,
                            Mennyiseg = 1,
                            Kodegyutt = p.Kodegyutt,
                            Megjegyzes = "pista visszahozott a raktar1-be 1 fekete sátrat",
                            Irany = Hely.Ki,
                            Hova = rak1,
                            HovaMennyiseg = 1
                        });
                    context.SaveChanges();
                    context.Hely.Add(
                        new Hely
                        {
                            Eszkoz = context.Eszkoz.First(c => c.Azonosito == "safeazon"),
                            Felhasznalo = felh,
                            EszkozHely = rak1,
                            Mikortol = new DateTime(2020, 04, 12),
                            Meddig = null,
                            Mennyiseg = 2,
                            Kodegyutt = p.Kodegyutt++,
                            Megjegyzes = "pista visszahozott a raktar1-be 1 fekete sátrat",
                            Irany = Hely.Be
                        });
                    context.SaveChanges();

                    ts.Complete();
                }
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using raktarProgram.Data;
using raktarProgram.Repositories;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;

namespace raktarProgramTests
{
    [TestClass]
    public class raktarRepotests
    {
        private string connstrin = "Server=(localdb)\\mssqllocaldb;Database=Raktarprog;Trusted_Connection=True;MultipleActiveResultSets=true";

        [TestMethod]
        public async Task TestMethod1()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);
            
      
            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);

            var xx = await hr.GetXMitList();
        }

        [TestMethod]
        public async Task AtadasTestNincsXMIT()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);


            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var xx = await hr.Xmentes(
                    null, 
                    null, 
                    null, 
                    DateTime.Now, 
                    0, 
                    null);

                Assert.AreEqual(HomeRespitory.nincsXmit, xx);
            }
        }

        [TestMethod]
        public async Task AtadasTestNincsXKitol()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);


            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);
            
            var eszkoz = (await hr.GetXMitList()).First();

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var xx = await hr.Xmentes(
                    eszkoz,
                    null,
                    null,
                    DateTime.Now,
                    0,
                    null);

                Assert.AreEqual(HomeRespitory.nincsXKitol, xx);
            }
        }

        [TestMethod]
        public async Task AtadasTestNincsHova()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);


            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);

            var eszkoz = (await hr.GetXMitList()).First();
            var kitol = (await hr.GetXKitolList(eszkoz.ID)).First();

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var xx = await hr.Xmentes(
                    eszkoz,
                    kitol,
                    null,
                    DateTime.Now,
                    0,
                    null);

                Assert.AreEqual(HomeRespitory.nincsXHova, xx);
            }
        }

        [TestMethod]
        public async Task AtadasTestRosszMennyiseg()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);


            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);

            var eszkoz = (await hr.GetXMitList()).First();
            var kitol = (await hr.GetXKitolList(eszkoz.ID)).First();
            var hova = (await hr.GetXHovaList(kitol.ID)).First();

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var xx = await hr.Xmentes(
                    eszkoz,
                    kitol,
                    hova,
                    DateTime.Now,
                    5000,
                    null);

                Assert.AreEqual(HomeRespitory.rosszMennyiseg, xx);
            }
        }

        [TestMethod]
        public async Task AtadasTestMentes()
        {
            var dbb = new DbContextOptionsBuilder<RaktarContext>();
            dbb.UseSqlServer(connstrin);


            RaktarContext rc = new RaktarContext(dbb.Options);
            HomeRespitory hr = new HomeRespitory(rc);

            var eszkoz = (await hr.GetXMitList()).First();
            var kitol = (await hr.GetXKitolList(eszkoz.ID)).First();
            var hova = (await hr.GetXHovaList(kitol.ID)).First();
            var mennyiseg = kitol.Mennyiseg - 1;

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var xx = await hr.Xmentes(
                    eszkoz,
                    kitol,
                    hova,
                    DateTime.Now,
                    mennyiseg,
                    null);

                Assert.AreEqual(HomeRespitory.sikeresFelvetel, xx);
            }
        }
    }
}

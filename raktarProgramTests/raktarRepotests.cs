using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using raktarProgram.Data;
using raktarProgram.Repositories;
using System;
using System.Linq;
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

                Assert.AreEqual(HomeRespitory.nincsXmit, xx.hiba);
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

                Assert.AreEqual(HomeRespitory.nincsXKitol, xx.hiba);
            }
        }
    }
}

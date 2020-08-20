using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IHomeRespitory
    {
        IQueryable<EszkozHely> EszkozHely { get; }
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }
        IQueryable<Eszkoz> Eszkoz { get; }
        IQueryable<Felhasznalo> Felhasznalo { get; }
        IQueryable<Hely> Hely { get; }

        Task<ListResult<Hely>> ListHome(HomeFilter filter, int pageSize, int pageNum);

        Task<List<Eszkoz>> GetXMitList();
        Task<List<Hely>> GetXKitolList(int eszkozID);

        Task<List<EszkozHely>> GetXHovaList(int eszkohelyID);
        Task<string> Xmentes(Eszkoz xmit, Hely xkitol, EszkozHely xhova, DateTime xmikor, int xmennyiseg, string xmegj);
    }
}
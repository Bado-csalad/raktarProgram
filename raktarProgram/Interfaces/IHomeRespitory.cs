using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IHomeRepository
    {
        IQueryable<EszkozHely> EszkozHely { get; }
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }
        IQueryable<Eszkoz> Eszkoz { get; }
        IQueryable<Felhasznalo> Felhasznalo { get; }
        IQueryable<Hely> Hely { get; }

        Task<ListResult<Hely>> ListHome(HomeFilter filter, int pageSize, int pageNum);

        Task<List<Eszkoz>> GetXMitList();
        Task<List<Hely>> GetXKitolList(int eszkozID);

        Task<List<Eszkoz>> GetXMitBeszerzesList();

        Task<List<EszkozHely>> GetXHovaList(int eszkohelyID);
        Task<string> Xmentes(Eszkoz xmit, Hely xkitol, EszkozHely xhova, DateTime xmikor, int xmennyiseg, string xmegj);
        Task<string> Xbeszerzes(Eszkoz xmit, EszkozHely xhova, DateTime xmikor, int xmennyiseg, string xmegj);
        Task<List<EszkozHely>> GetXHovaBeszerzesList();
        Task<ListResult<Hely>> ListBeszerzesek(BeszerzesFilter filter, int pageSize, int pageNum);
        Task<string> HelyModositas(Hely hely);
        Task<ListResult<Hely>> ListAtadasok(AtadasFilter filter, int pageSize, int pageNum);
        Task<List<EszkozHely>> GetXHovaAtadasokList();
    }
}
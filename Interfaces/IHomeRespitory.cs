using System.Linq;
using raktarProgram.Data;
using raktarProgram.Data.Filters;

namespace raktarProgram.Interfaces
{
    public interface IHomeRespitory
    {
        IQueryable<EszkozHely> EszkozHely { get; }
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }
        IQueryable<Eszkoz> Eszkoz { get; }
        IQueryable<Felhasznalo> Felhasznalo { get; }
        IQueryable<Hely> Hely { get; }

        IQueryable<Hely> ListHome(HomeFilter filter, int pageSize, int pageNum, out int totalCount, out IQueryable<Eszkoz> eszkozLista, out IQueryable<EszkozHely> eszkozHelyLista);
    }
}
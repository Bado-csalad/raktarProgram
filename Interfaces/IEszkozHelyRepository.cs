using System.Linq;
using raktarProgram.Data;
using raktarProgram.Data.Filters;

namespace raktarProgram.Interfaces
{
    public interface IEszkozHelyRepository
    {
        IQueryable<EszkozHely> EszkozHely { get; }
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }

        void EszkozHelyFelvetel(EszkozHely eszkozHely);
        void EszkozHelyTorles(int ID);
        void EszkozHelyModositas(EszkozHely eszkozHely);

        IQueryable<EszkozHely> ListEszkozHely(EszkozHelyFilter filter, int pageSize, int pageNum, out int totalCount);

    }
}
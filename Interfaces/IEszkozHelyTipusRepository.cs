using System.Linq;
using raktarProgram.Data;
using raktarProgram.Data.Filters;

namespace raktarProgram.Interfaces
{
    public interface IEszkozHelyTipusRepository
    {
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }

        void EszkozHelyTipusFelvetel(EszkozHelyTipus data);

        void EszkozHelyTipusTorles(int ID);
        void EszkozHelyTipusModositas(EszkozHelyTipus eszkozHelyTipus);
        IQueryable<EszkozHelyTipus> ListEszkozHelyTipus(EszkozHelyTipusFilter filter, int pageSize, int pageNum, out int totalCount);

    }
}
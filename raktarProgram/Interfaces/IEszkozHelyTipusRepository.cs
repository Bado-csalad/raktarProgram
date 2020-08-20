using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IEszkozHelyTipusRepository
    {
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }

        Task<EszkozHelyTipus> EszkozHelyTipusFelvetel(EszkozHelyTipus data);

        Task<EszkozHelyTipus> EszkozHelyTipusTorles(int ID);
        Task<EszkozHelyTipus> EszkozHelyTipusModositas(EszkozHelyTipus eszkozHelyTipus);
        Task<ListResult<EszkozHelyTipus>> ListEszkozHelyTipus(EszkozHelyTipusFilter filter, int pageSize, int pageNum);

    }
}
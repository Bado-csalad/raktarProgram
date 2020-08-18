using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IEszkozHelyRepository
    {
        IQueryable<EszkozHely> EszkozHely { get; }
        IQueryable<EszkozHelyTipus> EszkozHelyTipus { get; }

        Task<EszkozHely> EszkozHelyFelvetel(EszkozHely eszkozHely);
        Task<EszkozHely> EszkozHelyTorles(int ID);
        Task<EszkozHely> EszkozHelyModositas(EszkozHely eszkozHely);

        Task<ListResult<EszkozHely>> ListEszkozHely(EszkozHelyFilter filter, int pageSize, int pageNum);

    }
}
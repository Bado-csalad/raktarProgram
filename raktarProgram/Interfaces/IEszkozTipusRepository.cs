using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IEszkozTipusRepository
    {
        IQueryable<EszkozTipus> EszkozTipus { get; }

        Task<EszkozTipus> EszkozTipusFelvetel(EszkozTipus eszkozTipus);
        Task<EszkozTipus> EszkozTipusTorles(int ID);
        Task<EszkozTipus> EszkozTipusModositas(EszkozTipus eszkozTipus);

        Task<ListResult<EszkozTipus>> ListEszkozTipus(EszkozTipusFilter filter, int pageSize, int pageNum);
    }
}

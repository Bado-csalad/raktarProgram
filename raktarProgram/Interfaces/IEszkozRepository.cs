using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IEszkozRepository
    {
        IQueryable<Eszkoz> Eszkoz { get; }
        Task<Eszkoz> EszkozFelvetel(Eszkoz data);
        Task<Eszkoz> EszkozTorles(int ID);
        Task<Eszkoz> EszkozModositas(Eszkoz eszkoz);
        Task<ListResult<Eszkoz>> ListEszkoz(EszkozFilter filter, int pageSize, int pageNum);
    
    }
}
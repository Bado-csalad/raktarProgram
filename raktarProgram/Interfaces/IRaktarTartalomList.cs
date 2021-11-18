using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raktarProgram.Data;
using raktarProgram.Data.Filters;
using raktarProgram.Helpers;

namespace raktarProgram.Interfaces
{
    public interface IRaktarTartalomList
    {
        IQueryable<Eszkoz> Eszkoz { get; }
        IQueryable<EszkozHelyzet> HelyzetList { get; }

        Task<ListResult<Eszkoz>> ListEszkozHelyzet(string roviditett, int pageSize, int pageNum);
    }
}

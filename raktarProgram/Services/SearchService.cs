using raktarProgram.Data.Filters;

namespace raktarProgram.Services
{
    public class SearchService
    {
        public int PageSize { get; private set; } = 5;
        public EszkozFilter eszkozFilter { get; private set; } = new EszkozFilter();
        public EszkozHelyTipusFilter eszkozHelyTipusFilter { get; private set; } = new EszkozHelyTipusFilter();
    }
}
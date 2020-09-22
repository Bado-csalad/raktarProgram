using raktarProgram.Data.Filters;
using System;

namespace raktarProgram.Services
{
    public class SearchService
    {
        public int PageSize { get; private set; } = 5;
        public EszkozFilter eszkozFilter { get; private set; } = new EszkozFilter();
        public EszkozHelyTipusFilter eszkozHelyTipusFilter { get; private set; } = new EszkozHelyTipusFilter();
        public HomeFilter homeFilter { get; private set; } = new HomeFilter();
        public EszkozHelyFilter eszkozHelyFilter { get; private set; } = new EszkozHelyFilter();
        public BeszerzesFilter beszerezesFilter { get; private set; } = new BeszerzesFilter();
        public AtadasFilter atadasFilter { get; private set; } = new AtadasFilter();
        public EszkozTipusFilter eszkozTipusFilter { get; private set; } = new EszkozTipusFilter();
    }
}
using System.Collections.Generic;

namespace raktarProgram.Helpers
{
    public class ListResult<T>
    {
        public List<T> Data {get;set;}

        public int Total {get;set;}
    }
}
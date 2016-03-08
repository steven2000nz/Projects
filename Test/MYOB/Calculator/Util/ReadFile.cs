using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Util
{
    using FileHelpers;

    internal static class ReadFile<T> where T : class 
    {
        internal static List<T> GetData(string filePath)
        {
            var engine = new FileHelperEngine<T>();
            var records = engine.ReadFile(filePath);
            return records.ToList();
        }   
    }
}

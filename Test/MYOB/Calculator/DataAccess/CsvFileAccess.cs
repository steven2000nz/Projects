namespace Calculator.DataAccess
{
    using System.Collections.Generic;

    using Calculator.Interface;
    using Calculator.Util;

    using FileHelpers;

    public class CsvFileReadAccess<TIn> : FileReadAccess<TIn>
        where TIn : class
    {
        public override List<TIn> ReadFile(string filePath)
        {
            return ReadFile<TIn>.GetData(filePath);
        }
    }

    public class CsvFileWriteAccess<TOut> : FileWriteAccess<TOut>
        where TOut : class
    {
        public override void WriteFile(string filePath, List<TOut> data)
        {
            var engine = new FileHelperEngine<TOut> { HeaderText = typeof(TOut).GetCsvHeader() };

            engine.WriteFile(filePath, data);
        }
    }
}
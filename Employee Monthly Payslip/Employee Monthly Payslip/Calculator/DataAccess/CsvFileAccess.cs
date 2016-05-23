namespace Calculator.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Calculator.Interface;

    using FileHelpers;

    /// <summary>
    /// Read from CSV file
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public class CsvFileReadAccess<TIn> : FileReadAccess<TIn>, IFileReadAsyncAccess<TIn>
        where TIn : class
    {
        public override List<TIn> ReadFile(string filePath)
        {
            var engine = new FileHelperEngine<TIn>(Encoding.UTF8);
            var records = engine.ReadFile(filePath);
            return records.ToList();
        }

        /// <summary>
        /// Read one record at a time into memory
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="action"></param>
        public void ReadFileAsync(string filePath, Action<TIn> action)
        {
            var engine = new FileHelperAsyncEngine<TIn>(Encoding.UTF8);
            using (engine.BeginReadFile(filePath))
            {
                foreach (TIn record in engine)
                {
                    action(record);
                }
            }
        }
    }

    /// <summary>
    /// Write to CSV file
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
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
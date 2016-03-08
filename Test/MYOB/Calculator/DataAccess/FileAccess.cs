namespace Calculator.DataAccess
{
    using System.Collections.Generic;

    using Calculator.Interface;

    public abstract class FileReadAccess<TIn> : IFileReadAccess<TIn>
    {
        public abstract List<TIn> ReadFile(string filePath);
    }

    public abstract class FileWriteAccess<TOut> : IFileWriteAccess<TOut>
    {
        public abstract void WriteFile(string filePath, List<TOut> records);
    }
}
namespace Calculator.DataAccess
{
    using System.Collections.Generic;

    using Calculator.Interface;

    /// <summary>
    /// Read from file
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public abstract class FileReadAccess<TIn> : IFileReadAccess<TIn>
    {
        public abstract List<TIn> ReadFile(string filePath);
    }

    /// <summary>
    /// Write to file
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public abstract class FileWriteAccess<TOut> : IFileWriteAccess<TOut>
    {
        public abstract void WriteFile(string filePath, List<TOut> records);
    }
}
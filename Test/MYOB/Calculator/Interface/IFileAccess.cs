namespace Calculator.Interface
{
    using System;
    using System.Collections.Generic;

    public interface IFileReadAccess<TIn>
    {
        List<TIn> ReadFile(string filePath);
    }

    public interface IFileReadAsyncAccess<TIn> where TIn : class 
    {
        void ReadFileAsync(string filePath, Action<TIn> action);
    }

    public interface IFileWriteAccess<TOut>
    {
        void WriteFile(string filePath, List<TOut> records);
    }
}
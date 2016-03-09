namespace Calculator.Interface
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Read from file
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface IFileReadAccess<TIn>
    {
        List<TIn> ReadFile(string filePath);
    }

    /// <summary>
    /// Read from file one record at a time into memory
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface IFileReadAsyncAccess<TIn> where TIn : class 
    {
        void ReadFileAsync(string filePath, Action<TIn> action);
    }

    /// <summary>
    /// Write to file
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface IFileWriteAccess<TOut>
    {
        void WriteFile(string filePath, List<TOut> records);
    }
}
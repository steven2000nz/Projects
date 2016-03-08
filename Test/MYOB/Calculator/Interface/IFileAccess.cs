namespace Calculator.Interface
{
    using System.Collections.Generic;

    public interface IFileReadAccess<TIn>
    {
        List<TIn> ReadFile(string filePath);
    }

    public interface IFileWriteAccess<TOut>
    {
        void WriteFile(string filePath, List<TOut> records);
    }
}
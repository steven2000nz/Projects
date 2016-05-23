
namespace Calculator.Interface
{
    using System.Collections.Generic;

    using Calculator.Model;

    /// <summary>
    /// Calculate and generate output data.
    /// </summary>
    public interface ICalculationManager
    {
        OutputData GenerateOutputData(InputData inputData, List<Tax> taxList, ITaxManager manager);
    }
}

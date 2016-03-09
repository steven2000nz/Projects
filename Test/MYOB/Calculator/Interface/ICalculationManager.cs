
namespace Calculator.Interface
{
    using System.Collections.Generic;

    using Calculator.Model;

    public interface ICalculationManager
    {
        OutputData GenerateOutputData(InputData inputData, List<Tax> taxList, ITaxManager manager);
    }
}

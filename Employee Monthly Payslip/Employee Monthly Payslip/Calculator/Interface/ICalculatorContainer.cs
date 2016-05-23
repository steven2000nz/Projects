namespace Calculator.Interface
{
    using Castle.Windsor;

    /// <summary>
    /// Register components
    /// </summary>
    public interface ICalculatorContainer
    {
        WindsorContainer RegisterComponents();
    }
}
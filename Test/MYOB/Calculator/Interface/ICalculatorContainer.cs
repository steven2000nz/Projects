namespace Calculator.Interface
{
    using Castle.Windsor;

    public interface ICalculatorContainer
    {
        WindsorContainer RegisterComponents();
    }
}
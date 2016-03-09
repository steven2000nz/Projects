namespace Calculator.Register
{
    using System;

    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public class Container : ICalculatorContainer
    {
        public WindsorContainer RegisterComponents()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For(typeof(ICalculationManager)).ImplementedBy(typeof(CalculationManager)).LifestyleTransient());
            container.Register(
                Component.For(typeof(ITaxManager)).ImplementedBy(typeof(TaxManager)).LifestyleTransient());
            container.Register(
                Component.For(typeof(IFileReadAccess<>)).ImplementedBy(typeof(CsvFileReadAccess<>)).Named(CsvFileReadAccess).LifestyleTransient());
            container.Register(
                Component.For(typeof(IFileWriteAccess<>)).ImplementedBy(typeof(CsvFileWriteAccess<>)).Named(CsvFileWriteAccess).LifestyleTransient());
            container.Register(
                Component.For(typeof(IPayslipManager)).ImplementedBy(typeof(PayslipManager)).LifestyleTransient());

            Name = container.Name;

            return container;
        }

        public const string CsvFileReadAccess = "CsvFileReadAccess";
        public const string CsvFileWriteAccess = "CsvFileWriteAccess";
        public static string Name { get; set; }
    }
}
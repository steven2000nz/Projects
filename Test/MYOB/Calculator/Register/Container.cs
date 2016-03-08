namespace Calculator.Register
{
    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public class Container
    {
        public static void RegisterComponents(WindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IPayslipManager)).ImplementedBy(typeof(PayslipManager)).LifestyleTransient());
            container.Register(
                Component.For(typeof(ITaxManager)).ImplementedBy(typeof(TaxManager)).LifestyleTransient());
            container.Register(
                Component.For(typeof(IFileReadAccess<>)).ImplementedBy(typeof(CsvFileReadAccess<>)).Named("CsvFileReadAccess").LifestyleTransient());
            container.Register(
                Component.For(typeof(IFileWriteAccess<>)).ImplementedBy(typeof(CsvFileWriteAccess<>)).Named("CsvFileWriteAccess").LifestyleTransient());
        }
    }
}
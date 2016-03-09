using System;
using TechTalk.SpecFlow;

namespace UnitTestBDD
{
    using System.IO;
    using System.Linq;

    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Model;
    using Calculator.Register;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [Binding]
    public class MYOBFeatureSteps
    {
        string inputDataPath;
        string outputDataPath;
        string taxTablePath;

        [Given(@"I have entered input data path ""(.*)"" into the system")]
        public void GivenIHaveEnteredInputDataPathIntoTheSystem(string p0)
        {
            inputDataPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, p0);
        }
        
        [Given(@"I have entered output data path ""(.*)"" into the system")]
        public void GivenIHaveEnteredOutputDataPathIntoTheSystem(string p0)
        {
            outputDataPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, p0);
        }
        
        [Given(@"I have entered tax table path ""(.*)"" into the system")]
        public void GivenIHaveEnteredTaxTablePathIntoTheSystem(string p0)
        {
            taxTablePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, p0);
        }
        
        [When(@"I press enter")]
        public void WhenIPressEnter()
        {
            ICalculatorContainer calculatorContainer = new Container();
            var calculatorChildContainer = calculatorContainer.RegisterComponents();

            var payslipManager = calculatorChildContainer.Resolve<IPayslipManager>();
            payslipManager.GeneratePayslip(calculatorChildContainer, inputDataPath, outputDataPath, taxTablePath);
        }

        [Then(@"the result should be written to the output data path\.")]
        public void ThenTheResultShouldBeWrittenToTheOutputDataPath_()
        {
            IFileReadAccess<OutputData> fileReadResult = new CsvFileReadAccess<OutputData>();
            var firstRecord = fileReadResult.ReadFile(outputDataPath).First();

            Assert.AreEqual(firstRecord.IncomeTax, 922);
            Assert.AreEqual(firstRecord.GrossIncome, 5004);
            Assert.AreEqual(firstRecord.Super, 450);
            Assert.AreEqual(firstRecord.NetIncome, 4082);
        }
    }
}

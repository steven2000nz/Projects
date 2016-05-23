Feature: SpecFlowFeature
	1. Open command prompt window as administrator.
	2. Go to EmployeeMonthlyPayslip.exe directory and run it.
	3. Enter input data path.
    4. Enter output data path.
    5. Enter tax table path.
    6. Click enter.	

@mytag
Scenario: Generating Payslip
	Given I have entered input data path "UnitTestBDD\TestData\InputData.csv" into the system 
	And I have entered output data path "UnitTestBDD\TestData\OutputData.csv" into the system
	And I have entered tax table path "UnitTestBDD\TestData\TaxTable.csv" into the system
	When I press enter
	Then the result should be written to the output data path.
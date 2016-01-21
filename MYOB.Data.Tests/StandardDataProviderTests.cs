// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data.Tests
// StandardDataProviderTests.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace MYOB.Data.Tests
{
    /// <summary>
    /// Summary description for StandardDataProviderTests
    /// </summary>
    [TestClass]
    public class StandardDataProviderTests
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// File system
        /// </summary>
        public Mock<IFileSystem> FileSystem { get; set; }

        /// <summary>
        /// Logger
        /// </summary>
        public Mock<ILogger> Logger { get; set; }

        /// <summary>
        /// Correct CSV information
        /// </summary>
        public List<string> ReferenceCSVFile { get; set; }

        /// <summary>
        /// Correct employee list
        /// </summary>
        public List<IEmployee> ReferenceEmployeeList { get; set; }

        /// <summary>
        /// Wrong CSV information
        /// </summary>
        public List<string> IncorrectAnnualSalaryCSVFile { get; set; }

        /// <summary>
        /// Wrong CSV information
        /// </summary>
        public List<string> IncorrectSuperannuationRateCSVFile { get; set; }

        /// <summary>
        /// Wrong CSV information
        /// </summary>
        public List<string> IncorrectPaymentStartDateCSVFile { get; set; }

        #region Additional test attributes

        [TestInitialize]
        public void TestInitialize()
        {
            var correctLines = new string[] 
            { 
                "David,Rudd,60050,9%,01 March – 31 March", 
                "Ryan,Chen,120000,10%,01 March – 31 March" 
            };

            var incorrectAnnualSalaryLines = new string[] 
            { 
                "David,Rudd,xx_60050,9%,01 March – 31 March", 
                "Ryan,Chen,0,10%,01 March – 31 March",
                "Brian,Jones,10.95,10%,01 March – 31 March" 
            };

            var incorrectSuperannuationLines = new string[] 
            { 
                "David,Rudd,60050,9,01 March – 31 March", 
                "Ryan,Chen,120000,xxxx,01 March – 31 March",
                "Brian,Jones,10095,10%%,01 March – 31 March"  
            };

            var incorrectPaymentStartDateLines = new string[]
            {
                "David,Rudd,60050,9%,1 March – 31 March", 
                "Ryan,Chen,120000,10%,01 March–31 March",
                "Brian,Jones,120000,10%,01March – 31March",
                "David,Rudd,60050,9%,01 Barch – 31 Rarch" 
            };

            var employees = new IEmployee[]
            {
                new Employee()
                {
                    FisrtName = "David",
                    LastName = "Rudd",
                    AnnualSalary = 60050u,
                    SuperannuationRate = 0.09,
                    PaymentStartDate = new DateTime(2016, 3, 31)
                },
                new Employee()
                {
                    FisrtName = "Ryan",
                    LastName = "Chen",
                    AnnualSalary = 120000u,
                    SuperannuationRate = 0.1,
                    PaymentStartDate = new DateTime(2016, 3, 31)
                },
            };

            // Initialize
            FileSystem = new Mock<IFileSystem>();
            Logger = new Mock<ILogger>();
            Logger.Setup(x => x.Error(It.IsAny<string>()));
            Logger.Setup(x => x.Warning(It.IsAny<string>()));
            ReferenceCSVFile = new List<string>(correctLines);
            IncorrectAnnualSalaryCSVFile = new List<string>(incorrectAnnualSalaryLines);
            IncorrectSuperannuationRateCSVFile = new List<string>(incorrectAnnualSalaryLines);
            IncorrectPaymentStartDateCSVFile = new List<string>(incorrectPaymentStartDateLines);
            ReferenceEmployeeList = new List<IEmployee>(employees);
        }

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        /// <summary>
        /// Compare two employees
        /// </summary>
        /// <param name="employee1"></param>
        /// <param name="employee2"></param>
        /// <returns>True if they are equal</returns>
        private bool CompareEmployees(IEmployee employee1, IEmployee employee2)
        {
            if (employee1.FisrtName != employee2.FisrtName ||
                employee1.LastName != employee2.LastName ||
                employee1.AnnualSalary != employee2.AnnualSalary ||
                employee1.SuperannuationRate != employee2.SuperannuationRate ||
                employee1.PaymentStartDate != employee2.PaymentStartDate)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compare two employess lists
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        private bool CompareEmployeeLists(List<IEmployee> list1, List<IEmployee> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (var index=0; index<list1.Count; index++)
            {
                if (!CompareEmployees(list1[index], list2[index]))
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void InitializeDataSource_LoadReferenceCSV_Success()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            // Act
            dataProvider.InitializeDataSource("CorrectCVS.csv");
            var actualEmployeeList = new List<IEmployee>();
            dataProvider.GetEmployees(actualEmployeeList);

            // Assert
            Assert.IsTrue(CompareEmployeeLists(ReferenceEmployeeList, actualEmployeeList));
        }

        [TestMethod]
        public void InitializeDataSource_LoadCSVIncorrectAnnualSalary_Failure()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => IncorrectAnnualSalaryCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;
            dataProvider.Logger = Logger.Object;

            // Act
            dataProvider.InitializeDataSource("IncorrectAnnualSalaryCVS.csv");
            var actualEmployeeList = new List<IEmployee>();
            dataProvider.GetEmployees(actualEmployeeList);

            // Assert
            Assert.IsFalse(CompareEmployeeLists(ReferenceEmployeeList, actualEmployeeList));
        }

        [TestMethod]
        public void InitializeDataSource_LoadCSVIncorrectSuperannuationRate_Failure()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => IncorrectSuperannuationRateCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;
            dataProvider.Logger = Logger.Object;

            // Act
            dataProvider.InitializeDataSource("IncorrectSuperannuationRateCSV.csv");
            var actualEmployeeList = new List<IEmployee>();
            dataProvider.GetEmployees(actualEmployeeList);

            // Assert
            Assert.IsFalse(CompareEmployeeLists(ReferenceEmployeeList, actualEmployeeList));
        }

        [TestMethod]
        public void InitializeDataSource_LoadCSVIncorrectPaymentStartDate_Failure()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => IncorrectPaymentStartDateCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;
            dataProvider.Logger = Logger.Object;

            // Act
            dataProvider.InitializeDataSource("IncorrectPaymentStartDateCSV.csv");
            var actualEmployeeList = new List<IEmployee>();
            dataProvider.GetEmployees(actualEmployeeList);

            // Assert
            Assert.IsFalse(CompareEmployeeLists(ReferenceEmployeeList, actualEmployeeList));
        }

        [TestMethod]
        public void GetIncomeTax_SalaryLessThan18201_0()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            var annualSalary = 15000u;
            var expected = 0m;

            // Act
            var actual = dataProvider.GetIncomeTax(annualSalary);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetIncomeTax_SalaryEqualsTo20000_28()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            var annualSalary = 20000u;
            var expected = 28.5m;

            // Act
            var actual = dataProvider.GetIncomeTax(annualSalary);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetIncomeTax_SalaryEqualsTo50000_649()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            var annualSalary = 50000u;
            var expected = 649.75m;

            // Act
            var actual = dataProvider.GetIncomeTax(annualSalary);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetIncomeTax_SalaryEqualsTo100000_2078()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            var annualSalary = 100000u;
            var expected = 2078.9166m;
            expected = decimal.Round(expected, 2);

            // Act
            var actual = dataProvider.GetIncomeTax(annualSalary);
            actual = decimal.Round(actual, 2);

            // Assert
            Assert.IsTrue(decimal.Equals(expected, actual));
        }

        [TestMethod]
        public void GetIncomeTax_SalaryEqualsTo200000_5295()
        {
            // Arrange
            FileSystem.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(() => ReferenceCSVFile);

            var dataProvider = new StandardDataProvider();
            dataProvider.FileSystem = FileSystem.Object;

            var annualSalary = 200000u;
            var expected = 5295.5833m;
            expected = decimal.Round(expected, 2);

            // Act
            var actual = dataProvider.GetIncomeTax(annualSalary);
            actual = decimal.Round(actual, 2);

            // Assert
            Assert.IsTrue(decimal.Equals(expected, actual));
        }
    }
}

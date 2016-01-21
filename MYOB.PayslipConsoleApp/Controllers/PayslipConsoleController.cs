// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleController.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Controllers;
using MYOB.Core.Exceptions;
using MYOB.Core.Interfaces;
using MYOB.Core.Mef;
using MYOB.Data.Interfaces;
using MYOB.PayslipConsoleApp.Models;
using MYOB.PayslipConsoleApp.Properties;
using MYOB.PayslipConsoleApp.Views;
using MYOB.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp.Controllers
{
    /// <summary>
    /// Application controller
    /// </summary>
    internal class PayslipConsoleController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleController()
        {
            // Initialize instance variables
        }

        /// <summary>
        /// Data provider
        /// </summary>
        [Import(typeof(IStandardDataProvider))]
        private IStandardDataProvider DataProvider { get; set; }

        /// <summary>
        /// Standard IO
        /// </summary>
        [Import(typeof(IStandardIO))]
        private IStandardIO StandardIO { get; set; }

        /// <summary>
        /// File system
        /// </summary>
        [Import(typeof(IFileSystem))]
        private IFileSystem FileSystem { get; set; }

        /// <summary>
        /// Initialize command set
        /// </summary>
        protected override void InitializeCommandSet()
        {
            // Initialize command set
            Commands.Add("Start", StartCommand);
            Commands.Add("Stop", StopCommand);
            Commands.Add("Menu", MenuCommand);
            Commands.Add("Option", OptionCommand);
            Commands.Add("Quit", QuitCommand);
            Commands.Add("EmployeeName", EmployeeNameCommand);
            Commands.Add("SingleEmployee", SingleEmployeeCommand);
            Commands.Add("AllEmployees", AllEmployeesCommand);
        }

        /// <summary>
        /// Initialize model
        /// </summary>
        protected override void InitializeModel()
        {
            // Initialize data provider
            InitializeDataProvider();

            // Initialiaze the model
            Model = CompositeFactory<PayslipConsoleModel>.Instance.Create();

            // Set the data provider
            Model.DataProvider = DataProvider;
        }

        #region Private Methods

        /// <summary>
        /// Initialize data provider
        /// </summary>
        private void InitializeDataProvider()
        {
            try
            {
                // Initialization phase
                var basicDataFilePath = GetBasicDataFilePath();

                // Initialize the data provider
                DataProvider.InitializeDataSource(basicDataFilePath);
            }
            catch (Exception ex)
            {
                // Log it...
                Logger.Error(ex.Message);

                // Exception!
                throw new CoreException(ex.Message);
            }
        }

        /// <summary>
        /// Get basic data file location
        /// </summary>
        /// <returns></returns>
        private string GetBasicDataFilePath()
        {
            // Get assembly location
            var assemblyLocation = FileSystem.GetExecutingAssemblyLocation();

            if (assemblyLocation == null)
            {
                return null;
            }

            // Get basic data file path
            var basicDataPath = FileSystem.CombinePath(assemblyLocation, Resources.CONSOLE_DATA_FILENAME);

            return basicDataPath == null ? null : basicDataPath;
        }

        /// <summary>
        /// Get result payslip data file location
        /// </summary>
        /// <returns></returns>
        private string GetPayslipDataFilePath()
        {
            // Get assembly location
            var assemblyLocation = FileSystem.GetExecutingAssemblyLocation();

            if (assemblyLocation == null)
            {
                return null;
            }

            // Get basic data file path
            var basicDataPath = FileSystem.CombinePath(assemblyLocation, Resources.CONSOLE_PAYSLIP_FILENAME);

            return basicDataPath == null ? null : basicDataPath;
        }

        /// <summary>
        /// Start command
        /// </summary>
        /// <returns>View</returns>
        private IView StartCommand(object commandParameter = null)
        {
            // Create the view
            var view = CompositeFactory<PayslipConsoleStartView>.Instance.Create();

            return view;
        }

        /// <summary>
        /// Stop command
        /// </summary>
        /// <returns>View</returns>
        private IView StopCommand(object commandParameter = null)
        {
            // Create the view
            var view = CompositeFactory<PayslipConsoleStopView>.Instance.Create();

            return view;
        }

        /// <summary>
        /// Menu command
        /// </summary>
        /// <returns>View</returns>
        private IView MenuCommand(object commandParameter = null)
        {
            // Create the view
            var view = CompositeFactory<PayslipConsoleMenuView>.Instance.Create();

            return view;
        }

        /// <summary>
        /// Option command
        /// </summary>
        /// <returns>View</returns>
        private IView OptionCommand(object commandParameter = null)
        {
            var option = 1;

            do
            {
                // Check for the input...
                StandardIO.Write("Selected option: ");

                var optionSelected = StandardIO.ReadLine();

                if (string.IsNullOrEmpty(optionSelected))
                {
                    continue;
                }

                if (!int.TryParse(optionSelected, out option))
                {
                    continue;
                }

                if (option == 1 || option == 2)
                {
                    break;
                }

            } while (true);

            // Create the view
            var view = CompositeFactory<PayslipConsoleResultView>.Instance.Create();

            view.SetData("option", option);

            return view;
        }

        /// <summary>
        /// Quit command
        /// </summary>
        /// <returns>View</returns>
        private IView QuitCommand(object commandParameter = null)
        {
            var answer = "";

            StandardIO.WriteLine();

            do
            {
                // Check for the input...
                StandardIO.Write("Do you want to continue (yes/y | no/n)? : ");

                answer = StandardIO.ReadLine();

                if (CheckForValidCondition(answer))
                {
                    break;
                }

            } while (true);

            // Create the view
            var view = CompositeFactory<PayslipConsoleResultView>.Instance.Create();

            view.SetData("quit", answer);

            return view;
        }

        /// <summary>
        /// Employee name command
        /// </summary>
        /// <returns>View</returns>
        private IView EmployeeNameCommand(object commandParameter = null)
        {
            // Check for the input...
            StandardIO.WriteLine();

            StandardIO.Write("Enter first name: ");

            var firstName = StandardIO.ReadLine();

            StandardIO.Write("Enter last name: ");

            var lastName = StandardIO.ReadLine();

            // Pack the input
            var employeeName = new List<string>();

            employeeName.Add(firstName.Trim());
            employeeName.Add(lastName.Trim());

            // Create the view
            var view = CompositeFactory<PayslipConsoleResultView>.Instance.Create();

            view.SetData("employeeName", employeeName);

            return view;
        }

        /// <summary>
        /// Single employee command
        /// </summary>
        /// <returns>View</returns>
        private IView SingleEmployeeCommand(object commandParameter)
        {
            // Create the view
            var view = CompositeFactory<PayslipConsoleSingleEmployeeView>.Instance.Create();
            var employeeName = commandParameter as List<string>;
            var firstName = employeeName[0];
            var lastName = employeeName[1];
            var payslip = Model.CalculatePayslip(firstName, lastName);

            view.SetData("payslip", payslip);

            ToFile(payslip);

            return view;
        }

        /// <summary>
        /// All employees command
        /// </summary>
        /// <returns>View</returns>
        private IView AllEmployeesCommand(object commandParameter = null)
        {
            // Create the view
            var view = CompositeFactory<PayslipConsoleAllEmployeesView>.Instance.Create();
            var payslips = Model.CalculatePayslips();

            view.SetData("payslips", payslips);

            ToFile(payslips);

            return view;
        }

        /// <summary>
        /// Check for termination condition
        /// </summary>
        /// <param name="termination"></param>
        /// <returns>True for terminate, other false</returns>
        private bool CheckForValidCondition(string termination)
        {
            if (string.IsNullOrEmpty(termination))
            {
                return false;
            }

            var condition = termination.Trim().ToLower();

            return condition.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                   condition.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                   condition.Equals("no", StringComparison.OrdinalIgnoreCase) ||
                   condition.Equals("n", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Export payslip information to file
        /// </summary>
        /// <param name="payslip"></param>
        private void ToFile(IPayslip payslip)
        {
            try
            {
                if (payslip == null)
                {
                    return;
                }

                var filePath = GetPayslipDataFilePath();

                FileSystem.WriteAllText(filePath, payslip.ToString());
            }
            catch (Exception ex)
            {
                // Log it...
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Export payslips information to file
        /// </summary>
        /// <param name="payslips"></param>
        private void ToFile(List<IPayslip> payslips)
        {
            try
            {
                if (payslips == null || payslips.Count == 0)
                {
                    return;
                }

                var filePath = GetPayslipDataFilePath();
                var payslipsInfo = new List<string>();

                payslips.ForEach(p => payslipsInfo.Add(p.ToString()));

                FileSystem.WriteAllLines(filePath, payslipsInfo.ToArray());
            }
            catch (Exception ex)
            {
                // Log it...
                Logger.Error(ex.Message);
            }
        }

        #endregion
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleApp.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Controllers.Interfaces;
using MYOB.Core.Interfaces;
using MYOB.Core.Mef;
using MYOB.PayslipConsoleApp.Controllers;
using System;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp
{
    /// <summary>
    /// Payslip console application class
    /// </summary>
    internal class PayslipConsoleApp : IComposite
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleApp()
        {
            // Initialize instance variables
            Controller = CompositeFactory<PayslipConsoleController>.Instance.Create();
            Controller.Initialize();
        }

        /// <summary>
        /// Standard IO
        /// </summary>
        [Import(typeof(IStandardIO))]
        private IStandardIO StandardIO { get; set; }

        /// <summary>
        /// Logger component
        /// </summary>
        [Import(typeof(ILogger))]
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Application controller
        /// </summary>
        private IController Controller { get; set; }

        /// <summary>
        /// Open application
        /// </summary>
        public void Open()
        {
            // <-- Initialization phase -->
            ProcessCommand("Start");
        }

        /// <summary>
        /// Run application
        /// </summary>
        public void Run()
        {
            // <-- Execution phase -->
            var quit = "y";

            do
            {
                // Show menu
                ProcessCommand("Menu");

                // Select option
                var option = (int)ProcessInput("Option", "option");

                // Check option
                if (option == 1)
                {
                    var employeeName = ProcessInput("EmployeeName", "employeeName");

                    ProcessCommand("SingleEmployee", employeeName);
                }
                else
                {
                    ProcessCommand("AllEmployees");
                }

                // Check for termination...
                quit = (string)ProcessInput("Quit", "quit");

            } while (CheckTerminationCondition(quit));
        }

        /// <summary>
        /// Close application
        /// </summary>
        public void Close()
        {
            // <-- Finalization phase -->
            ProcessCommand("Stop");
        }

        #region Private Methods

        /// <summary>
        /// Process command
        /// </summary>
        /// <param name="commandName"></param>
        private void ProcessCommand(string commandName)
        {
            // Process command
            var view = Controller.ProcessCommand(commandName);

            // Check the returning view...
            if (view == null)
            {
                return;
            }

            // Render view
            view.Render();
        }

        /// <summary>
        /// Process command
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandParameter"></param>
        private void ProcessCommand(string commandName, object commandParameter)
        {
            // Process command
            var view = Controller.ProcessCommand(commandName, commandParameter);

            // Check the returning view...
            if (view == null)
            {
                return;
            }

            // Render view
            view.Render();
        }

        /// <summary>
        /// Process input
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        private object ProcessInput(string commandName, string resultName)
        {
            // Process command
            var view = Controller.ProcessCommand(commandName);

            // Check the returning view...
            if (view == null)
            {
                return null;
            }

            // Get result
            return view.GetData(resultName);
        }

        /// <summary>
        /// Check for termination condition
        /// </summary>
        /// <param name="termination"></param>
        /// <returns>True for terminate, other false</returns>
        private bool CheckTerminationCondition(string termination)
        {
            if (string.IsNullOrEmpty(termination))
            {
                return false;
            }

            var condition = termination.Trim().ToLower();

            return condition.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                   condition.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}

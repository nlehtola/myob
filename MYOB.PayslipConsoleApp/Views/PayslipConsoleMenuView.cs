// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleMenuView.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Models.Interfaces;
using MYOB.Views;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp.Views
{
    /// <summary>
    /// View class
    /// </summary>
    internal class PayslipConsoleMenuView : View
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleMenuView()
        {
            // Initialize instance variables
        }

        /// <summary>
        /// Standard IO
        /// </summary>
        [Import(typeof(IStandardIO))]
        private IStandardIO StandardIO { get; set; }

        /// <summary>
        /// Render data in the view
        /// </summary>
        public override void Render()
        {
            StandardIO.WriteLine();
            StandardIO.WriteLine("Select one of the options:");
            StandardIO.WriteLine();
            StandardIO.WriteLine(" 1. Payslip for a specific employee");
            StandardIO.WriteLine(" 2. Payslip for all employees");
            StandardIO.WriteLine();
        }
    }
}

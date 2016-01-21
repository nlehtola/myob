// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleSingleEmployeeView.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using MYOB.Views;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp.Views
{
    /// <summary>
    /// View class
    /// </summary>
    internal class PayslipConsoleSingleEmployeeView : View
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleSingleEmployeeView()
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
            var payslip = GetData("payslip") as IPayslip;

            // Check for data value
            if (payslip == null)
            {
                StandardIO.WriteLine();
                StandardIO.WriteLine("=> The employee is not listed in the payroll");

                return;
            }

            // Render it...
            StandardIO.WriteLine();
            StandardIO.WriteLine("=> The payslip is:");
            StandardIO.WriteLine(payslip.ToString());
            StandardIO.WriteLine();
        }
    }
}

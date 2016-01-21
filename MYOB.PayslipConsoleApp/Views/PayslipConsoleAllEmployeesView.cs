// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleAllEmployeesView.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using MYOB.Views;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp.Views
{
    /// <summary>
    /// View class
    /// </summary>
    internal class PayslipConsoleAllEmployeesView : View
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleAllEmployeesView()
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
            var payslips = GetData("payslips") as List<IPayslip>;

            // Check for data value
            if (payslips == null)
            {
                return;
            }

            // Render it...
            StandardIO.WriteLine();
            StandardIO.WriteLine(payslips.Count > 1 ? "=> The payslips are:" : "=> The payslip is:");

            payslips.ForEach(p => StandardIO.WriteLine(p.ToString()));

            StandardIO.WriteLine();
        }
    }
}

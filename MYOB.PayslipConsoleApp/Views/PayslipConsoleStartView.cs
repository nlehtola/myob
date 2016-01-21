// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// PayslipConsoleStartView.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Views;
using System.ComponentModel.Composition;

namespace MYOB.PayslipConsoleApp.Views
{
    /// <summary>
    /// View class
    /// </summary>
    internal class PayslipConsoleStartView : View
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PayslipConsoleStartView()
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
            StandardIO.WriteLine("Payslip Pro - 2016 (c) DRNL");
            StandardIO.WriteLine("===========================");
            StandardIO.WriteLine();
        }
    }
}

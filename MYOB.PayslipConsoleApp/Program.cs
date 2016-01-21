// ---------------------------------------------------------------------------------
// MYOB - MYOB.PayslipConsoleApp
// Program.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Mef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYOB.PayslipConsoleApp
{
    /// <summary>
    /// MYOB payslip console application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create the application
            var payslipApp = CompositeFactory<PayslipConsoleApp>.Instance.Create();

            // Open it...
            payslipApp.Open();

            // Run it...
            payslipApp.Run();

            // Close it...
            payslipApp.Close();

        }
    }
}

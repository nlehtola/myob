// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// IPayslip.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using System;

namespace MYOB.Data.Interfaces
{
    /// <summary>
    /// Interface for Payslip objects
    /// </summary>
    public interface IPayslip
    {
        /// <summary>
        /// Full name
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// LPay period
        /// </summary>
        string PayPeriod { get; set; }

        /// <summary>
        /// Gross income
        /// </summary>
        uint GrossIncome { get; set; }

        /// <summary>
        /// Net income
        /// </summary>
        uint NetIncome { get; set; }

        /// <summary>
        /// Income tax
        /// </summary>
        uint IncomeTax { get; set; }

        /// <summary>
        /// Superannuation
        /// </summary>
        uint Superannuation { get; set; }

        /// <summary>
        /// Return the summary of the payslip
        /// </summary>
        /// <returns>Summary of the payslip</returns>
        string ToString();
    }
}

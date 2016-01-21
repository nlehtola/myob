// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// IEmployee.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using System;

namespace MYOB.Data.Interfaces
{
    /// <summary>
    /// Interface for Employee objects
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// First name
        /// </summary>
        string FisrtName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Annual salary
        /// </summary>
        uint AnnualSalary { get; set; }

        /// <summary>
        /// Superannuation rate
        /// </summary>
        double SuperannuationRate { get; set; }

        /// <summary>
        /// Payment start date
        /// </summary>
        DateTime PaymentStartDate { get; set; }
    }
}

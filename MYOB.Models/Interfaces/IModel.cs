// ---------------------------------------------------------------------------------
// MYOB - MYOB.Models
// IModel.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Data.Interfaces;
using System.Collections.Generic;

namespace MYOB.Models.Interfaces
{
    /// <summary>
    /// Interface for Model objects
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Data provider
        /// </summary>
        IDataProvider DataProvider { get; set; }

        /// <summary>
        /// Calculate the payslip of an employee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Employee's payslip</returns>
        IPayslip CalculatePayslip(string firstName, string lastName);

        /// <summary>
        /// Calculate the payslips of all employees
        /// </summary>
        /// <returns>Employees' payslips</returns>
        List<IPayslip> CalculatePayslips();
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// IDataProvider.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MYOB.Data.Interfaces
{
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Find an employee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        IEmployee FindEmployee(string firstName, string lastName);

        /// <summary>
        /// Get employees
        /// </summary>
        /// <param name="employees"></param>
        void GetEmployees(List<IEmployee> employees);

        /// <summary>
        /// Get income tax
        /// </summary>
        /// <param name="annualSalary"></param>
        /// <returns>Income tax</returns>
        decimal GetIncomeTax(uint annualSalary);
    }
}

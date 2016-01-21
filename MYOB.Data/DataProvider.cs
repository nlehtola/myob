// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// DataProvider.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MYOB.Data
{
    /// <summary>
    /// Core data provider class
    /// </summary>
    public abstract class DataProvider : IDataProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProvider()
        {
            // Initialize instance variables
            Employees = new List<IEmployee>();
        }

        /// <summary>
        /// Logger component
        /// </summary>
        [Import(typeof(ILogger))]
        internal ILogger Logger { get; set; }

        /// <summary>
        /// Employees
        /// </summary>
        protected List<IEmployee> Employees { get; set; }

        /// <summary>
        /// Find an employee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Target employee or null</returns>
        public IEmployee FindEmployee(string firstName, string lastName)
        {
            var employee = Employees.FirstOrDefault(e =>
                e.FisrtName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                e.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            return employee;
        }

        /// <summary>
        /// Get employees
        /// </summary>
        /// <param name="employees"></param>
        public void GetEmployees(List<IEmployee> employees)
        {
            // Clear the container
            employees.Clear();

            // Populate the container
            Employees.ForEach(e => employees.Add(e));
        }

        /// <summary>
        /// Get income tax
        /// </summary>
        /// <param name="annualSalary"></param>
        /// <returns>Income tax</returns>
        public abstract decimal GetIncomeTax(uint annualSalary);

        #region Protected Methods


        #endregion
    }
}

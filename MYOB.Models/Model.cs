// ---------------------------------------------------------------------------------
// MYOB - MYOB.Models
// Model.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Exceptions;
using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using MYOB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MYOB.Models
{
    /// <summary>
    /// Model class
    /// </summary>
    public abstract class Model : IModel, IComposite
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Model()
        {
            // Initialize instance variables
            DataProvider = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataProvider"></param>
        public Model(IDataProvider dataProvider)
        {
            // Pre-condition: The data provider can't be null!
            if (dataProvider == null)
            {
                throw new CoreException("Model error: the data provider can't be null");
            }

            // Initialize instance variables
            DataProvider = dataProvider;
        }
        
        /// <summary>
        /// Entity factory
        /// </summary>
        [Import(typeof(IEntityFactory))]
        public IEntityFactory EntityFactory { get; set; }

        /// <summary>
        /// Logger
        /// </summary>
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }

        /// <summary>
        /// Data provider
        /// </summary>
        public IDataProvider DataProvider { get; set; }

        /// <summary>
        /// Calculate the payslip of an employee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Employee's payslip</returns>
        public IPayslip CalculatePayslip(string firstName, string lastName)
        {
            var employee = DataProvider.FindEmployee(firstName, lastName);

            // Found it?
            if (employee == null)
            {
                return null;
            }

            // Calculate the payslip
            var payslip = CalculatePayslip(employee);

            return payslip;
        }

        /// <summary>
        /// Calculate the payslips of all employees
        /// </summary>
        /// <returns>Employees' payslips</returns>
        public List<IPayslip> CalculatePayslips()
        {
            var employees = new List<IEmployee>();

            // Get all employees
            DataProvider.GetEmployees(employees);

            // Is there any?
            if (employees.Count == 0)
            {
                return new List<IPayslip>();
            }

            // Calculate the payslips
            var payslips = CalculatePayslips(employees);

            return payslips;
        }

        #region Private Methods

        /// <summary>
        /// Calculate the payslip of an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's payslip</returns>
        private IPayslip CalculatePayslip(IEmployee employee)
        {
            try
            {
                var payslip = EntityFactory.CreatePayslip();

                // Populate payslip entity
                payslip.FullName = GetFullName(employee);
                payslip.GrossIncome = GetGrossIncome(employee);
                payslip.NetIncome = GetNetIncome(employee);
                payslip.IncomeTax = GetIncomeTax(employee);
                payslip.PayPeriod = GetPayPeriod(employee);
                payslip.Superannuation = GetSuperannuation(employee);

                return payslip;
            }
            catch (Exception ex)
            {
                // Log it
                Logger.Error(ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Calculate the payslip of an employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns>Employees' payslips</returns>
        private List<IPayslip> CalculatePayslips(List<IEmployee> employees)
        {
            var payslips = new List<IPayslip>();

            // Calculate payslips
            employees.ForEach(e =>
            {
                var payslip = CalculatePayslip(e);

                if (payslip != null)
                {
                    payslips.Add(payslip);
                }
            });

            return payslips;
        }

        /// <summary>
        /// Get employee's income tax
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's income tax</returns>
        private uint GetIncomeTax(IEmployee employee)
        {
            var precIncomeTax = DataProvider.GetIncomeTax(employee.AnnualSalary);

            precIncomeTax = decimal.Round(precIncomeTax, 0, MidpointRounding.AwayFromZero);

            var incomeTax = (uint)precIncomeTax;

            return incomeTax;
        }

        /// <summary>
        /// Get employee's full name
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's full name</returns>
        private string GetFullName(IEmployee employee)
        {
            var fullName = string.Format("{0} {1}", employee.FisrtName, employee.LastName);

            return fullName;
        }

        /// <summary>
        /// Get employee's net income
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's net income</returns>
        private uint GetNetIncome(IEmployee employee)
        {
            var netIncome = GetGrossIncome(employee) - GetIncomeTax(employee);

            return netIncome;
        }

        /// <summary>
        /// Get employee's gross income
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's gross income</returns>
        private uint GetGrossIncome(IEmployee employee)
        {
            var precGrossIncome = (decimal)employee.AnnualSalary;

            precGrossIncome = decimal.Divide(precGrossIncome, 12.0m);

            precGrossIncome = decimal.Round(precGrossIncome, 0, MidpointRounding.AwayFromZero);

            var grossIncome = (uint)precGrossIncome;

            return grossIncome;
        }

        /// <summary>
        /// Get employee's superannuation
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's superannuation</returns>
        private uint GetSuperannuation(IEmployee employee)
        {
            var grossIncome = GetGrossIncome(employee);
            var superannuationRate = (decimal)employee.SuperannuationRate;
            var precSupperannuation = decimal.Multiply(grossIncome, superannuationRate);

            precSupperannuation = decimal.Round(precSupperannuation, 0, MidpointRounding.AwayFromZero);

            var supperannuation = (uint)precSupperannuation;

            return supperannuation;
        }

        /// <summary>
        /// Get employee's pay period
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee's pay period</returns>
        private string GetPayPeriod(IEmployee employee)
        {
            var paymentStartDate = employee.PaymentStartDate;
            var firstDay = new DateTime(paymentStartDate.Year, paymentStartDate.Month, 1);
            var lastDay = new DateTime(paymentStartDate.Year, paymentStartDate.Month, 
                              DateTime.DaysInMonth(paymentStartDate.Year, paymentStartDate.Month));
            var payPeriod = string.Format("{0} - {1}", firstDay.ToString("dd MMMM"), lastDay.ToString("dd MMMM"));

            return payPeriod;
        }

        #endregion
    }
}

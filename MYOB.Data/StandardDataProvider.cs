// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// StandardDataProvider.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Data.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MYOB.Data
{
    /// <summary>
    /// Standard data provider class
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IStandardDataProvider))]
    public class StandardDataProvider : DataProvider, IStandardDataProvider
    {
        /// <summary>
        /// File system
        /// </summary>
        [Import(typeof(IFileSystem))]
        internal IFileSystem FileSystem { get; set; }

        /// <summary>
        /// Initialize data source
        /// </summary>
        /// <param name="csvFilePath"></param>
        public void InitializeDataSource(string csvFilePath)
        {
            // Reset current employee container (just in case...)
            Employees.Clear();

            // Read contents of the file
            var lines = FileSystem.ReadAllLines(csvFilePath);

            // Add the employees to the container
            lines.ForEach(l => 
            {
                var employee = CreateEmployee(l);

                if (employee != null)
                {
                    Employees.Add(employee);
                }
            });
        }

        /// <summary>
        /// Get income tax
        /// </summary>
        /// <param name="annualSalary"></param>
        /// <returns>Income tax</returns>
        public override decimal GetIncomeTax(uint annualSalary)
        {
            //Taxable income   Tax on this income
            //0 - $18,200     Nil
            //$18,201 - $37,000       19c for each $1 over $18,200
            //$37,001 - $80,000       $3,572 plus 32.5c for each $1 over $37,000


            //$80,001 - $180,000      $17,547 plus 37c for each $1 over $80,000

            //$180,001 and over       $54,547 plus 45c for each $1 over $180,000
            //The tax table is from ATO: http://www.ato.gov.au/content/12333.htm

            var incomeTax = 0.0m;

            if (annualSalary <= 18200u)
            {
                incomeTax = 0.0m;
            }
            else if (annualSalary <= 37000u)
            {
                incomeTax = decimal.Subtract((decimal)annualSalary, 18200m);
                incomeTax = decimal.Multiply(incomeTax, 0.19m);
            }
            else if (annualSalary <= 80000u)
            {
                incomeTax = decimal.Subtract((decimal)annualSalary, 37000m);
                incomeTax = decimal.Multiply(incomeTax, 0.325m);
                incomeTax = decimal.Add(incomeTax, 3572m);
            }
            else if (annualSalary <= 180000u)
            {
                incomeTax = decimal.Subtract((decimal)annualSalary, 80000m);
                incomeTax = decimal.Multiply(incomeTax, 0.37m);
                incomeTax = decimal.Add(incomeTax, 17547m);
            }
            else
            {
                incomeTax = decimal.Subtract((decimal)annualSalary, 180000m);
                incomeTax = decimal.Multiply(incomeTax, 0.45m);
                incomeTax = decimal.Add(incomeTax, 54547m);
            }

            incomeTax = decimal.Divide(incomeTax, 12m);

            return incomeTax;
        }

        #region Private Methods

        /// <summary>
        /// Create an employee from a string
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <returns></returns>
        private IEmployee CreateEmployee(string employeeInfo)
        {
            // Note: Sample of the CSV lines
            // David,Rudd,60050,9%,01 March – 31 March
            // Ryan,Chen,120000,10%,01 March – 31 March

            // Sanity check
            if (string.IsNullOrEmpty(employeeInfo))
            {
                return null;
            }

            // Parse the string
            try
            {
                var tokens = employeeInfo.Split(new char[] { ',' });

                // Number of tokens must be 5
                if (tokens.Length != 5)
                {
                    return null;
                }

                // Get the employee information
                var strValue = "";

                // First name...
                var firstName = tokens[0].Trim();

                // Last name...
                var lastName = tokens[1].Trim();

                // Annual salary...
                strValue = tokens[2].Trim();

                var annualSalary = GetAnnualSalary(strValue);

                if (!annualSalary.HasValue)
                {
                    // Log it
                    Logger.Error(string.Format("Annual salary is not valid ({0})!", tokens[2]));

                    return null;
                }

                // Superannuation rate...
                strValue = tokens[3].Trim();

                var superannuationRate = GetSuperannuationRate(strValue);

                if (!superannuationRate.HasValue)
                {
                    // Log it
                    Logger.Error(string.Format("Superannuation rate is not valid ({0})!", tokens[3]));

                    return null;
                }

                // Payment start date...
                strValue = tokens[4].Trim();

                var paymentStartDate = GetPaymentStartDate(strValue);

                if (!paymentStartDate.HasValue)
                {
                    // Log it
                    Logger.Error(string.Format("Payment start date is not valid ({0})!", tokens[4]));

                    return null;
                }

                // Create employee
                var employee = new Employee()
                {
                    FisrtName = firstName,
                    LastName = lastName,
                    AnnualSalary = annualSalary.Value,
                    SuperannuationRate = superannuationRate.Value,
                    PaymentStartDate = paymentStartDate.Value
                };

                return employee;
            }
            catch (Exception ex)
            {
                // Log it
                Logger.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Get payment start date from a string
        /// </summary>
        /// <param name="payPeriodStr"></param>
        /// <returns>Payment start date</returns>
        private DateTime? GetPaymentStartDate(string payPeriodStr)
        {
            try
            {
                // Note: Pattern => 01 March – 31 March
                var pattern = @"[0-9][0-9][\s][a-zA-Z]+[\s]*[\p{Pd}][\s]*[0-9][0-9][\s][a-zA-Z]+";


                if (!Regex.IsMatch(payPeriodStr, pattern, RegexOptions.IgnoreCase))
                {
                    return null;
                }

                var targetMonth = GetTargetMonth(payPeriodStr);

                if (targetMonth == 0)
                {
                    return null;
                }

                var targetYear = DateTime.Today.Year;
                var targetDay = DateTime.DaysInMonth(targetYear, targetMonth);
                var date = new DateTime(targetYear, targetMonth, targetDay);

                return date;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get target month
        /// </summary>
        /// <param name="payPeriodStr"></param>
        /// <returns>Month index (1 to 12)</returns>
        private int GetTargetMonth(string payPeriodStr)
        {
            var months = new string[] 
            { 
                "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" 
            };
            var index = 0;

            foreach(var month in months)
            {
                if (payPeriodStr.IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ++index;
                }

                index ++;
            };

            return index;
        }

        /// <summary>
        /// Get the superannuation rate from a string
        /// </summary>
        /// <param name="superannuationRateStr"></param>
        /// <returns>Superannuation rate</returns>
        private double? GetSuperannuationRate(string superannuationRateStr)
        {
            try
            {
                var pattern = @"^?[0-9]*\.?[0-9][%]";

                if (!Regex.IsMatch(superannuationRateStr, pattern, RegexOptions.IgnoreCase))
                {
                    return null;
                }

                superannuationRateStr = Regex.Replace(superannuationRateStr, "%", "", RegexOptions.None).Trim();

                var superannuationRate = 0.0;

                if (!double.TryParse(superannuationRateStr, out superannuationRate))
                {
                    return null;
                }

                if (superannuationRate < 0.0 || superannuationRate > 50.0)
                {
                    return null;
                }

                superannuationRate /= 100.0;

                return superannuationRate;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get annual salary from a string
        /// </summary>
        /// <param name="annualSalaryStr"></param>
        /// <returns>Annual salary</returns>
        private uint? GetAnnualSalary(string annualSalaryStr)
        {
            try
            {
                var annualSalary = 1u;

                if (!uint.TryParse(annualSalaryStr, out annualSalary) || annualSalary == 0)
                {
                    return null;
                }

                return annualSalary;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}

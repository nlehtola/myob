// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// Payslip.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Data.Interfaces;
using System;
using System.Xml.Serialization;

namespace MYOB.Data
{
    /// <summary>
    /// Payslip class
    /// </summary>
    [Serializable()]
    public class Payslip : IPayslip
    {
        /// <summary>
        ///  Constructor
        /// </summary>
        public Payslip()
        {
            // Initialize instance variables
            FullName = string.Empty;
            PayPeriod = string.Empty;
            GrossIncome = 0u;
            NetIncome = 0u;
            IncomeTax = 0u;
            Superannuation = 0u;
        }

        /// <summary>
        /// Full name
        /// </summary>
        [XmlElement("FullName")]
        public string FullName { get; set; }

        /// <summary>
        /// LPay period
        /// </summary>
        [XmlElement("PayPeriod")]
        public string PayPeriod { get; set; }

        /// <summary>
        /// Gross income
        /// </summary>
        [XmlElement("GrossIncome")]
        public uint GrossIncome { get; set; }

        /// <summary>
        /// Net income
        /// </summary>
        [XmlElement("NetIncome")]
        public uint NetIncome { get; set; }

        /// <summary>
        /// Income tax
        /// </summary>
        [XmlElement("IncomeTax")]
        public uint IncomeTax { get; set; }

        /// <summary>
        /// Superannuation
        /// </summary>
        [XmlElement("Superannuation")]
        public uint Superannuation { get; set; }

        /// <summary>
        /// Return the summary of the payslip
        /// </summary>
        /// <returns>Summary of the payslip</returns>
        public override string ToString()
        {
            var info = string.Format("{0},{1},{2},{3},{4},{5}",
                FullName,
                PayPeriod,
                GrossIncome,
                IncomeTax,
                NetIncome,
                Superannuation);

            return info;
        }
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// Employee.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Data.Interfaces;
using System;
using System.Xml.Serialization;

namespace MYOB.Data
{
    /// <summary>
    /// Employee class
    /// </summary>
    [Serializable()]
    public class Employee : IEmployee
    {
        /// <summary>
        ///  Constructor
        /// </summary>
        public Employee()
        {
            // Initialize instance variables
            FisrtName = string.Empty;
            LastName = string.Empty;
            AnnualSalary = 0;
            SuperannuationRate = 0.0;
            PaymentStartDate = DateTime.Now;
        }

        /// <summary>
        /// First name
        /// </summary>
        [XmlElement("FisrtName")]
        public string FisrtName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [XmlElement("LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Annual salary
        /// </summary>
        [XmlElement("AnnualSalary")]
        public uint AnnualSalary { get; set; }

        /// <summary>
        /// Superannuation rate
        /// </summary>
        [XmlElement("SuperannuationRate")]
        public double SuperannuationRate { get; set; }

        /// <summary>
        /// Payment start date
        /// </summary>
        [XmlElement("PaymentStartDate")]
        public DateTime PaymentStartDate { get; set; }
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// EntityFactory.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Data.Interfaces;
using System.ComponentModel.Composition;

namespace MYOB.Data
{
    /// <summary>
    /// Entity factory class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IEntityFactory))]
    public class EntityFactory : IEntityFactory
    {
        /// <summary>
        /// Create payslip
        /// </summary>
        /// <returns>Payslip</returns>
        public IPayslip CreatePayslip()
        {
            // Create entity
            return new Payslip();
        }

        /// <summary>
        /// Create employee
        /// </summary>
        /// <returns>Employee</returns>
        public IEmployee CreateEmployee()
        {
            // Create entity
            return new Employee();
        }
    }
}

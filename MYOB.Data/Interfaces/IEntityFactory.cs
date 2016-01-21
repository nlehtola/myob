// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// IEntityFactory.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

namespace MYOB.Data.Interfaces
{
    /// <summary>
    /// Entity factory interface
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        /// Create payslip
        /// </summary>
        /// <returns>Payslip</returns>
        IPayslip CreatePayslip();

        /// <summary>
        /// Create employee
        /// </summary>
        /// <returns>Employee</returns>
        IEmployee CreateEmployee();
    }
}

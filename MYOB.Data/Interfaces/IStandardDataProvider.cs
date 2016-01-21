// ---------------------------------------------------------------------------------
// MYOB - MYOB.Data
// IDataProvider.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

namespace MYOB.Data.Interfaces
{
    /// <summary>
    /// Standard data provider interface
    /// </summary>
    public interface IStandardDataProvider : IDataProvider
    {
        /// <summary>
        /// Initialize data source
        /// </summary>
        /// <param name="csvFilePath"></param>
        void InitializeDataSource(string csvFilePath);
    }
}

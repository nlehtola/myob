// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// ILogger.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

namespace MYOB.Core.Interfaces
{
    /// <summary>
    /// Interface for log components
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a message in the trace file
        /// </summary>
        /// <param name="message">Message</param>
        void Trace(string message);

        /// <summary>
        /// Writes a message in the log file
        /// </summary>
        /// <param name="message">Message</param>
        void Warning(string message);

        /// <summary>
        /// Writes a message in the log file
        /// </summary>
        /// <param name="message">Message</param>
        void Error(string message);

        /// <summary>
        /// Writes a message in the log file
        /// </summary>
        /// <param name="message">Message</param>
        void Console(string message);
    }
}
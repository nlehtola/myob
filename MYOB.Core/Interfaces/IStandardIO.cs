// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// IStandardIO.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MYOB.Core.Interfaces
{
    /// <summary>
    /// Interface for the standard IO functionality
    /// </summary>
    public interface IStandardIO
    {
        /// <summary>
        /// Print a value on the standard output device
        /// </summary>
        /// <param name="value"></param>
        void Write(string value);

        /// <summary>
        /// Print a value on the standard output device
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string value);

        /// <summary>
        /// Print an empty line on the standard output device
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Read a line from the standard input device
        /// </summary>
        string ReadLine();
    }
}


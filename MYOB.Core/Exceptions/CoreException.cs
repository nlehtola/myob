// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// CoreException.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using System;

namespace MYOB.Core.Exceptions
{
    /// <summary>
    /// Core exception class
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// StandardIO.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace MYOB.Core.IO
{
    /// <summary>
    /// File system class
    /// </summary>
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IStandardIO))]
    public class StandardIO : IStandardIO
    {
        /// <summary>
        /// Display a value on the stadard output device
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            // Print the value
            Console.Write(value);
        }

        /// <summary>
        /// Display a value on the stadard output device
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            // Print the value
            Console.WriteLine(value);
        }

        /// <summary>
        /// Print an empty line on the standard output device
        /// </summary>
        public void WriteLine()
        {
            // Print the value
            Console.WriteLine();
        }

        /// <summary>
        /// Read a line from the standard input device
        /// </summary>
        public string ReadLine()
        {
            // Read a line
            var line = Console.ReadLine();

            return line;
        }
    }
}

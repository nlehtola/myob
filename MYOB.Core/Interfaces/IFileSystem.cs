// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// IFileSystem.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MYOB.Core.Interfaces
{
    /// <summary>
    /// Interface for the file system functionality
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Read all lines from text file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Array with the file contents</returns>
        List<string> ReadAllLines(string path);

        /// <summary>
        /// Get executing assembly location
        /// </summary>
        /// <returns></returns>
        string GetExecutingAssemblyLocation();

        /// <summary>
        /// Combine path
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="additionalPath"></param>
        /// <returns></returns>
        string CombinePath(string rootPath, string additionalPath);

        /// <summary>
        /// Write all lines in the text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lines"></param>
        void WriteAllLines(string filePath, string[] lines);

        /// <summary>
        /// Write all text in the text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lines"></param>
        void WriteAllText(string filePath, string text);
    }
}

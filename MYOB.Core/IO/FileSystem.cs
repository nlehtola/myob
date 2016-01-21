// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// FileSystem.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;

namespace MYOB.Core.IO
{
    /// <summary>
    /// File system class
    /// </summary>
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IFileSystem))]
    public class FileSystem : IFileSystem
    {
        /// <summary>
        /// Read all the lines from text file 
        /// (each line represents an element in the container)
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Array with the file contents</returns>
        public List<string> ReadAllLines(string path)
        {
            var lines = new List<string>();

            try
            {
                var linesFromFile = File.ReadAllLines(path);

                foreach (var line in linesFromFile)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                }
            }
            catch
            {
            }

            return lines;
        }

        /// <summary>
        /// Get executing assembly location
        /// </summary>
        /// <returns>Path of the executing assembly</returns>
        public string GetExecutingAssemblyLocation()
        {
            // Get location
            try
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Combine path
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns>Combined path (path1 "+" path2)</returns>
        public string CombinePath(string path1, string path2)
        {
            try
            {
                return Path.Combine(path1, path2);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Write all lines in the text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lines"></param>
        public void WriteAllLines(string filePath, string[] lines)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Write all text in the text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lines"></param>
        public void WriteAllText(string filePath, string text)
        {
            try
            {
                File.WriteAllText(filePath, text);
            }
            catch
            {
            }
        }
    }
}

// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// Logger.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Core.Properties;
using System;
using System.ComponentModel.Composition;
using System.IO;

namespace MYOB.Core.Log
{
    /// <summary>
    /// General logger
    /// </summary>
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        /// <summary>
        /// Constructor
        /// </summary>
        static Logger()
        {
            // Initialize instance variables
            LockObject = new object();

            SetFileNames();
        }

        /// <summary>
        /// Lock
        /// </summary>
        private static object LockObject { get; set; }

        /// <summary>
        /// Trace file
        /// </summary>
        private static string LogFile { get; set; }

        /// <summary>
        /// Trace file
        /// </summary>
        private static string TraceFile { get; set; }

        /// <summary>
        /// Writes a message in the trace file
        /// </summary>
        /// <param name="message">Message</param>
        public void Trace(string message)
        {
#if (DEBUG && NO_LOG)
            lock (LockObject)
            {
                LogMessage(TraceFile, Resources.CS_CORE_TRACE_LEVEL, message);
            }
#endif
        }

        /// <summary>
        /// Writes a message in the log file
        /// </summary>
        /// <param name="message">Message</param>
        public void Warning(string message)
        {
#if (DEBUG && NO_LOG)
            lock (LockObject)
            {
                LogMessage(LogFile, Resources.CS_CORE_WARNING_LEVEL, message);
            }
#endif
        }

        /// <summary>
        /// Writes a message in the log file
        /// </summary>
        /// <param name="message">Message</param>
        public void Error(string message)
        {
#if (DEBUG && NO_LOG)
            lock (LockObject)
            {
                LogMessage(LogFile, Resources.CS_CORE_ERROR_LEVEL, message);
            }
#endif
        }

        /// <summary>
        /// Writes a message in the console
        /// </summary>
        /// <param name="message">Message</param>
        public void Console(string message)
        {
            lock (LockObject)
            {
                System.Console.WriteLine(ComposeMessage("+", message));
            }
        }

        /// <summary>
        /// Log the message in the given file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private static void LogMessage(string fileName, string level, string message)
        {
            try
            {
                var msg = ComposeMessage(level, message);

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Compose the log message base on the original one
        /// </summary>
        /// <param name="original">Original message</param>
        /// <returns>Composed message</returns>
        private static string ComposeMessage(string level, string original)
        {
            return string.Format("{0} [{1}] - {2}", DateTime.Now.ToString("HH:mm:ss tt zz"), level.ToUpper(), original);
        }

        /// <summary>
        /// Set file names used in the logging process
        /// </summary>
        private static void SetFileNames()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDataFolder = Path.Combine(appDataFolder, Resources.MYOB_CORE_FOLDER_NAME);

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }

            // Trace
            TraceFile = Path.Combine(appDataFolder,
                Resources.MYOB_CORE_TRACE_FILENAME + "-" + DateTime.Now.ToString("yy-MM-dd") + Resources.MYOB_CORE_TRACE_EXTENSION);

            // Log
            LogFile = Path.Combine(appDataFolder,
                Resources.MYOB_CORE_LOG_FILENAME + "-" + DateTime.Now.ToString("yy-MM-dd") + Resources.MYOB_CORE_LOG_EXTENSION);
        }
    }
}
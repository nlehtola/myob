// ---------------------------------------------------------------------------------
// MYOB - MYOB.Controllers
// IController.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Views.Interfaces;

namespace MYOB.Controllers.Interfaces
{
    /// <summary>
    /// Interface definition for controllers
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Initialize the controller and its components
        /// </summary>
        void Initialize();

        /// <summary>
        /// Process a specified command
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>View to render the results or null (if it fails!)</returns>
        IView ProcessCommand(string commandName);

        /// <summary>
        /// Process a specified command
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandParameter"></param>
        /// <returns>View to render the results or null (if it fails!)</returns>
        IView ProcessCommand(string commandName, object commandParameter);
    }
}

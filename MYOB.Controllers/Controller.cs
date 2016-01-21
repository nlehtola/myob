// ---------------------------------------------------------------------------------
// MYOB - MYOB.Controllers
// Controller.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Controllers.Interfaces;
using MYOB.Core.Interfaces;
using MYOB.Models.Interfaces;
using MYOB.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MYOB.Controllers
{
    /// <summary>
    /// Controller class
    /// </summary>
    public abstract class Controller : IController, IComposite
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected Controller()
        {
            // Initialize instance variables
            Model = null;   // Note: It must be initialized in the base class!
            Commands = new Dictionary<string, Func<object, IView>>();
        }

        /// <summary>
        /// Logger
        /// </summary>
        [Import(typeof(ILogger))]
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        protected IModel Model { get; set; }

        /// <summary>
        /// Commands
        /// </summary>
        protected Dictionary<string, Func<object, IView>> Commands { get; private set; }

        /// <summary>
        /// Initialize command set
        /// </summary>
        protected abstract void InitializeCommandSet();

        /// <summary>
        /// Initialize model
        /// </summary>
        protected abstract void InitializeModel();

        /// <summary>
        /// Initialize the controller and its components
        /// </summary>
        public void Initialize()
        {
            // Initialize controller's command set
            InitializeCommandSet();

            // Initialize controller's model
            InitializeModel();
        }

        /// <summary>
        /// Process a specified command
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>View to render the results or null (if it fails!)</returns>
        public IView ProcessCommand(string commandName)
        {
            try
            {
                var command = Commands.FirstOrDefault(p => p.Key.Equals(commandName, StringComparison.OrdinalIgnoreCase)).Value;

                if (command == null)
                {
                    // Log it...
                    Logger.Error("Action associated to the command doesn't exist");

                    return null;
                }

                return command(null);
            }
            catch (Exception ex)
            {
                // Log it...
                Logger.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Process a specified command
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="commandParameter"></param>
        /// <returns>View to render the results or null (if it fails!)</returns>
        public IView ProcessCommand(string commandName, object commandParameter)
        {
            try
            {
                var command = Commands.FirstOrDefault(p => p.Key.Equals(commandName, StringComparison.OrdinalIgnoreCase)).Value;

                if (command == null)
                {
                    // Log it...
                    Logger.Error("Action associated to the command doesn't exist");

                    return null;
                }

                return command(commandParameter);
            }
            catch (Exception ex)
            {
                // Log it...
                Logger.Error(ex.Message);
            }

            return null;
        }
    }
}

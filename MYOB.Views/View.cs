// ---------------------------------------------------------------------------------
// MYOB - MYOB.Views
// View.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using MYOB.Models.Interfaces;
using MYOB.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYOB.Views
{
    /// <summary>
    /// Controller class
    /// </summary>
    public abstract class View : IView, IComposite
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected View()
        {
            // Initialize instance variables
            Data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Results obtained from the model (model data)
        /// </summary>
        protected Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        protected IModel Model { get; set; }

        /// <summary>
        /// Render data in the view
        /// </summary>
        public virtual void Render()
        {
            // Do nothing for now... default behavior...
        }

        /// <summary>
        /// Update data (based on the model)
        /// </summary>
        public virtual void Update(IModel model)
        {
            // Just update the model reference...
            Model = model;
        }

        /// <summary>
        /// Access specific results (model data) from the view
        /// </summary>
        public object GetData(string dataKey)
        {
            try
            {
                var dataValue = Data.FirstOrDefault(p => p.Key.Equals(dataKey, StringComparison.OrdinalIgnoreCase)).Value;

                return dataValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set specific result in the view
        /// </summary>
        public void SetData(string dataKey, object dataValue)
        {
            try
            {
                if (Data.ContainsKey(dataKey))
                {
                    Data[dataKey] = dataValue;
                }
                else
                {
                    Data.Add(dataKey, dataValue);
                }
            }
            catch
            {

            }
        }
    }
}

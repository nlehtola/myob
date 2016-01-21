// ---------------------------------------------------------------------------------
// MYOB - MYOB.Views
// IController.cs
// DRNL
// 2016.01.19 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Models.Interfaces;

namespace MYOB.Views.Interfaces
{
    /// <summary>
    /// Interface definition for views
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Render data in the view
        /// </summary>
        void Render();

        /// <summary>
        /// Access specific results (model data) from the view
        /// </summary>
        object GetData(string dataKey);

        /// <summary>
        /// Set specific result in the view
        /// </summary>
        void SetData(string dataKey, object dataValue);

        /// <summary>
        /// Update data (based on the model)
        /// </summary>
        void Update(IModel Model);
    }
}

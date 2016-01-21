// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// CompositeFactory.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Extensions;
using MYOB.Core.Interfaces;

namespace MYOB.Core.Mef
{
    /// <summary>
    /// Composite factory class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeFactory<T> where T : IComposite, new()
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static CompositeFactory()
        {
            // Initialize static instance variables
            Instance = new CompositeFactory<T>();
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static CompositeFactory<T> Instance { get; private set; }

        /// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of type T</returns>
        public T Create()
        {
            var obj = new T();

            obj.ResolveParts();

            return obj;
        }
    }
}
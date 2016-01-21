// ---------------------------------------------------------------------------------
// MYOB - MYOB.Core
// Composite.cs
// DRNL
// 2016.01.18 (c) DRNL
// ---------------------------------------------------------------------------------

using MYOB.Core.Interfaces;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace MYOB.Core.Extensions
{
    /// <summary>
    /// Class used for define extension methods for IComposite objects
    /// </summary>
    public static class CompositeExtensions
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static CompositeExtensions()
        {
            // Initialize instance variables
            CompositionContainer = null;
        }

        /// <summary>
        /// Main composition container
        /// </summary>
        private static CompositionContainer CompositionContainer { get; set; }

        /// <summary>
        /// Resolve parts of the composite object
        /// </summary>
        public static void ResolveParts(this IComposite composite)
        {
            if (CompositionContainer == null)
            {
                // An aggregate catalog that combines multiple catalogs
                var catalog = new AggregateCatalog();

                // Add all the parts found in all assemblies in the same directory as the executing program
                catalog.Catalogs.Add(
                    new DirectoryCatalog(
                        Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location
                            )
                        )
                    );

                // Create the CompositionContainer with the parts in the catalog.
                CompositionContainer = new CompositionContainer(catalog);
            }

            // Fill the imports of this object
            CompositionContainer.ComposeParts(composite);
        }
    }
}
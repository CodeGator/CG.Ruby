
#region Local using statements
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
#endregion

namespace EnvDTE
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="DTE"/>
    /// type.
    /// </summary>
    public static partial class DTEExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method performs a recursive walkthrough of the current Visual
        /// Studio solution, building up a list of project names, namespaces,
        /// and class names.
        /// </summary>
        /// <param name="dte">The Visual Studio automation object to use for
        /// the operation.</param>
        /// <param name="table">The results of the operation, a table of project
        /// names, that contains a table of namesspaces, that contains a list 
        /// of class names.</param>
        /// <returns>the value of the <paramref name="dte"/> parameter, for 
        /// chaining method calls together, Fluent style.</returns>
        /// <exception cref="COMException">This exception is thrown whenever
        /// the Visual Studio automation object decides to barf all over the 
        /// place, for whatever reason.</exception>
        public static DTE FindClassesInSolution(
            this DTE dte,
            Dictionary<string, Dictionary<string, List<string>>> table
            )
        {
            // Make the compiler happy.
            ThreadHelper.ThrowIfNotOnUIThread();

            // Loop through the projects in the solution.
            var projCount = dte.Solution.Projects.Count;
            for (var x = 1; x <= projCount; x++)
            {
                // Get the next project in the solution.
                var project = dte.Solution.Projects.Item(x);

                // Add a record to the table for this project.
                table[project.Name] = new Dictionary<string, List<string>>();

                // Walk down through the tree.
                dte.FindClassesInProject(project, table);
            }

            // Return the DTE.
            return dte;
        }

        // *******************************************************************

        /// <summary>
        /// This method performs a recursive walkthrough of the current Visual
        /// Studio project, building up a list of namespaces and class names.
        /// </summary>
        /// <param name="dte">The Visual Studio automation object to use for
        /// the operation.</param>
        /// <param name="project">The project to recursively walkthrough.</param>
        /// <param name="table">The results of the operation, a table of project
        /// names, that contains a table of namesspaces, that contains a list 
        /// of class names.</param>
        /// <returns>the value of the <paramref name="dte"/> parameter, for 
        /// chaining method calls together, Fluent style.</returns>
        /// <exception cref="COMException">This exception is thrown whenever
        /// the Visual Studio automation object decides to barf all over the 
        /// place, for whatever reason.</exception>
        public static DTE FindClassesInProject(
            this DTE dte, 
            Project project,
            Dictionary<string, Dictionary<string, List<string>>> table
            )
        {
            // Make the compiler happy.
            ThreadHelper.ThrowIfNotOnUIThread();

            // Loop through any sub-items in this project.
            var itemCount = project.ProjectItems.Count;
            for (var x = 1; x <= itemCount; x++)
            {
                // Walk down through the tree.
                var projItem = project.ProjectItems.Item(x);
                dte.FindClassesInProjectItem(projItem, table);
            }

            // Return the DTE.
            return dte;
        }

        // *******************************************************************

        /// <summary>
        /// This method performs a recursive walkthrough of the current Visual
        /// Studio project item, building up a list of class names.
        /// </summary>
        /// <param name="dte">The Visual Studio automation object to use for
        /// the operation.</param>
        /// <param name="projItem">The project item to recursively walkthrough.</param>
        /// <param name="table">The results of the operation, a table of project
        /// names, that contains a table of namesspaces, that contains a list 
        /// of class names.</param>
        /// <returns>the value of the <paramref name="dte"/> parameter, for 
        /// chaining method calls together, Fluent style.</returns>
        /// <exception cref="COMException">This exception is thrown whenever
        /// the Visual Studio automation object decides to barf all over the 
        /// place, for whatever reason.</exception>
        public static DTE FindClassesInProjectItem(
            this DTE dte, 
            ProjectItem projItem,
            Dictionary<string, Dictionary<string, List<string>>> table
            )
        {
            // Make the compiler happy.
            ThreadHelper.ThrowIfNotOnUIThread();

            // Loop through any sub-items in this project item.
            var subItemCount = projItem.ProjectItems.Count;
            for (var x = 1; x <= subItemCount; x++)
            {
                // Walk down through the tree.
                var subItem = projItem.ProjectItems.Item(x);
                dte.FindClassesInProjectItem(subItem, table);
            }

            // Look for a namespace in the code-elements, for this project item.
            var codeNamespace = projItem.FileCodeModel?.CodeElements?.Cast<CodeElement>()
                    .OfType<CodeNamespace>().FirstOrDefault();

            // Did we find one?
            if (null != codeNamespace)
            {
                // Add a namespace to the table, for this project.
                //table[projItem.ContainingProject.Name][projItem.Name] = new List<string>();

                // Look for a code-type in the members, for this namespace.
                var codeType = codeNamespace.Members?.Cast<CodeElement>()
                    .OfType<CodeType>().FirstOrDefault();

                // Did we find one?
                if (null != codeType)
                {
                    // Get property(s) we care about.
                    var className = codeType.Name;
                    var classNamespace = codeType.Namespace.Name;

                    // Get the name of the parent project.
                    var projName = projItem.ContainingProject.Name;

                    // Get the table for the parent project.
                    var projTable = table[projName];

                    // Should we add the namespace?
                    if (!projTable.ContainsKey(classNamespace))
                    {
                        // Add a blank list for the namespace.
                        projTable[classNamespace] = new List<string>();
                    }

                    // Add the class to the list, for this namespace.
                    projTable[classNamespace].Add(className);
                }
            }

            // Return the DTE.
            return dte;
        }

        #endregion
    }
}


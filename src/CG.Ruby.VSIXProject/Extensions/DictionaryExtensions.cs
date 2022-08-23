
#region Local using statements
using System;
#endregion

namespace System.Collections.Generic
{ 
    /// <summary>
    /// This class contains extension methods related to the <see cref="Dictionary{Key, Value}"/>
    /// type.
    /// </summary>
    public static partial class DictionaryExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method walks the dictionary looking for the RepositoryException
        /// class.
        /// </summary>
        /// <param name="table">The table to search.</param>
        /// <returns>True if the class exists in the table; false otherwise.</returns>
        public static bool RepositoryExceptionFound(
            this Dictionary<string, Dictionary<string, List<string>>> table
            )
        {
            // Loop through the project keys.
            foreach (var projKey in table.Keys)
            {
                // Loop through the namespace keys.
                foreach (var nsKey in table[projKey].Keys)
                {
                    // Loop through the classes.
                    foreach (var className in table[projKey][nsKey])
                    {
                        // Did we find a match?
                        if ("RepositoryException" == className)
                        {
                            return true;
                        }
                    }
                }
            }

            // Not found.
            return false;
        }

        #endregion
    }
}

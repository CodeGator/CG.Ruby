
#region Local using statements
using System;
#endregion

namespace CG.Ruby.UI.ViewModels 
{
    /// <summary>
    /// This enumeration contains the valid repository types.
    /// </summary>
    public enum RepositoryTypes
    {
        /// <summary>
        /// This value indicates the wizard should generate the default repository type
        /// </summary>
        Default,

        /// <summary>
        /// This value indicates the wizard should generate the EFORE repository type
        /// </summary>
        EfCore
    }
}

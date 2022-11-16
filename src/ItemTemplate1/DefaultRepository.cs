#region Local using statements
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using $modelnamespace$;
#endregion

namespace $newnamespace$
{
	/// <summary>
	/// This class is a default implementation of the <see cref="$newifacename$"/>
	/// interface.
	/// </summary>
	internal class $newclassname$ : $newifacename$
	{
		// *******************************************************************
        // Public methods.
        // *******************************************************************
		
		#region Public methods
		$if$ ($addanyasync$ == true)
		/// <inheritdoc/>
		public virtual async Task<bool> AnyAsync(
            Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$
		// *******************************************************************
		$if$ ($addcreateasync$ == true)
		/// <inheritdoc/>
		public virtual async Task<$modelclass$> CreateAsync(
			$modelclass$ model,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$
		// *******************************************************************
		$if$ ($adddeleteasync$ == true)
		/// <inheritdoc/>
		public virtual async Task DeleteAsync(
			$modelclass$ model,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$
		// *******************************************************************
		$if$ ($addfindasync$ == true)
		/// <inheritdoc/>
		public virtual async Task<IEnumerable<$modelclass$>> FindAsync(
            Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$
		// *******************************************************************
		$if$ ($addfindsingleasync$ == true)
		/// <inheritdoc/>
		public virtual async Task<$modelclass$?> FindSingleAsync(
            Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$
		// *******************************************************************
		$if$ ($addupdateasync$ == true)
		/// <inheritdoc/>
		public virtual async Task<$modelclass$> UpdateAsync(
			$modelclass$ model,
            CancellationToken cancellationToken = default
            )
		{
			// TODO : write the code for this.
			throw new NotImplementedException();
		}
		$endif$		
		#endregion
	}
}

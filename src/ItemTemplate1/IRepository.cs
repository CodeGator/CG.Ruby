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
	/// This interface represents an object that manages the storage and retrieval
	/// of <see cref="$modelclass$"/> objects.
	/// </summary>
	public interface $newifacename$
	{
	$if$ ($addanyasync$ == true)
		/// <summary>
		/// This method indicates whether there are any <see cref="$modelclass$"/> objects 
		/// in the underlying storage.
		/// </summary>
		/// <param name="expression">The LINQ expression to use for the search.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation that returns <c>true</c> if there 
		/// are any <see cref="$modelclass$"/> objects that match the given LINQ 
		/// expression; <c>false</c> otherwise.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task<bool> AnyAsync(
            Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            );
		$if$ ($addcreateasync$ == true)
		/// <summary>
		/// This method creates a new <see cref="$modelclass$"/> object in the 
		/// underlying storage.
		/// </summary>
		/// <param name="model">The model to create in the underlying storage.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation that returns the newly created
		/// <see cref="$modelclass$"/> object.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task<$modelclass$> CreateAsync(
			$modelclass$ model,
			CancellationToken cancellationToken = default
			);
		$endif$ $if$ ($adddeleteasync$ == true)
		/// <summary>
		/// This method deletes an existing <see cref="$modelclass$"/> object from the 
		/// underlying storage.
		/// </summary>
		/// <param name="model">The model to delete from the underlying storage.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task DeleteAsync(
			$modelclass$ model,
            CancellationToken cancellationToken = default
            );
		$endif$ $if$ ($addfindasync$ == true)
		/// <summary>
		/// This method searches for matching <see cref="$modelclass$"/> objects using
		/// the given LINQ expression.
		/// </summary>
		/// <param name="expression">The LINQ expression to use for the search.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation that returns the results of the search.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task<IEnumerable<$modelclass$>> FindAsync(
            Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            );
		$endif$ $if$ ($addfindsingleasync$ == true)
		/// <summary>
		/// This method searches for a single matching <see cref="$modelclass$"/> object using
		/// the given LINQ expression.
		/// </summary>
		/// <param name="expression">The LINQ expression to use for the search.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation that returns a matching <see cref="$modelclass$"/> 
		/// object, if one was found, or NULL otherwise.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task<$modelclass$?> FindSingleAsync(
			Expression<Func<$modelclass$, bool>> expression,
            CancellationToken cancellationToken = default
            );
		$endif$ $if$ ($addupdateasync$ == true)
		/// <summary>
		/// This method updates an existing <see cref="$modelclass$"/> object in the 
		/// underlying storage.
		/// </summary>
		/// <param name="model">The model to update in the underlying storage.</param>
		/// <param name="cancellationToken">A cancellation token that is monitored
		/// for the lifetime of the method.</param>
		/// <returns>A task to perform the operation that returns the newly updated
		/// <see cref="$modelclass$"/> object.</returns>
		/// <exception cref="ArgumentException">This exception is thrown whenever one
		/// or more arguments are missing, or invalid.</exception>
		/// <exception cref="RepositoryException">This exception is thrown whenever the
		/// repository fails to complete the operation.
		Task<$modelclass$> UpdateAsync(
			$modelclass$ model,
            CancellationToken cancellationToken = default
            );
		$endif$
	}
}

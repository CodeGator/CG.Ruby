
#region Local using statements
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using $modelnamespace$;
using $efcorecontextnamespace$;
#endregion

namespace $newnamespace$
{
	/// <summary>
	/// This class is an EFCORE implementation of the <see cref="$newifacename$"/>
	/// interface.
	/// </summary>
	internal class $newclassname$ : $newifacename$
	{
		// *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields
        
		/// <summary>
        /// This field contains the EFCORE data-context for this repository.
        /// </summary>
		protected readonly $efcorecontextclass$ _dbContext;

        /// <summary>
        /// This field contains the logger for this repository.
        /// </summary>
        protected readonly ILogger<$newclassname$> _logger;

        #endregion

		// *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

		/// <summary>
        /// This constructor creates a new instance of the <see cref="$newclassname$"/>
        /// class.
        /// </summary>
        /// <param name="dbContext">The EFCORE data-context to use with this 
		/// repository.</param>
		/// <param name="logger">The logger to use with this repository.</param>
		public $newclassname$(
            $efcorecontextclass$ dbContext,
			ILogger<$newclassname$> logger
            )
		{
			// Save the reference(s).
			_dbContext = dbContext;
            _logger = logger;
		}

		#endregion

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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
                    "Creating a new $modelclass$ instance in the " +
					"$efcorecontextclass$ data-context"
                    );

				// Create the entity in the data-store.
				var data = await _dbContext.Set<$modelclass$>().AnyAsync(
					expression,
					cancellationToken
					).ConfigureAwait(false);

				// Return the results.
				return data;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
                    "Failed to search for matching $modelclass$ instances in " +
					"the $efcorecontextclass$ data-context"
                    );

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for matching " +
					"$modelclass$ instances in the $efcorecontextclass$ data-context!",
					innerException: ex
					);
			}
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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
                    "Creating a new $modelclass$ instance in the " +
					"$efcorecontextclass$ data-context"
                    );

				// Create the entity in the data-store.
				var entity = await _dbContext.Set<$modelclass$>()
					.AddAsync(
						model,
						cancellationToken
						).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
                    "Saving changes to the $efcorecontextclass$ data-context"
                    );

				// Save the changes.
				await _dbContext.SaveChangesAsync(
		            cancellationToken
			        ).ConfigureAwait(false);

				// Return the results.
				return entity.Entity;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
                    "Failed to create a $modelclass$ instance in the " +
					"$efcorecontextclass$ data-context"
                    );

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to create a $modelclass$ " +
					"instance in the $efcorecontextclass$ data-context!",
					innerException: ex
					);
			}
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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
					"Removing a $modelclass$ instance from the " +
					"$efcorecontextclass$ data-context"
					);

				// Remove the entity from the data-store.
				var entity = _dbContext.Set<$modelclass$>()
					.Remove(model);

				// Log what we are about to do.
				_logger.LogDebug(
					"Saving changes to the $efcorecontextclass$ data-context"
					);

				// Save the changes.
				await _dbContext.SaveChangesAsync(
					cancellationToken
					).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to remove a $modelclass$ instance from the " +
					"$efcorecontextclass$ data-context"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to remove a $modelclass$ " +
					"instance from the $efcorecontextclass$ data-context!",
					innerException: ex
					);
			}
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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
                    "Searching for matching $modelclass$ instances from" +
					"a $efcorecontextclass$ data-context"
                    );

				// Perform the search.
				var data = await _dbContext.Set<$modelclass$>().Where(
						expression
						).ToListAsync(
							cancellationToken
							).ConfigureAwait(false);

				// Return the result.
				return data;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to search for matching $modelclass$ instances from " +
					"the $efcorecontextclass$ data-context"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for matching " +
					$"$modelclass$ instances from the $efcorecontextclass$ " +
					"data-context!",
					innerException: ex
					);
			}
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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
					"Searching for a matching $modelclass$ instance from" +
					"a $efcorecontextclass$ data-context"
					);

				// Perform the search.
				var data = await _dbContext.Set<$modelclass$>().Where(
					expression
					).FirstOrDefaultAsync(
					    cancellationToken
						).ConfigureAwait(false);

				// Return the result.
				return data;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to search for a matching $modelclass$ instance from " +
					"the $efcorecontextclass$ data-context"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for a matching " +
					$"$modelclass$ instance from the $efcorecontextclass$ " +
					"data-context!",
					innerException: ex
					);
			}
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
			try
			{
				// Log what we are about to do.
				_logger.LogDebug(
					"Updating a $modelclass$ instance from the " +
					"$efcorecontextclass$ data-context"
					);

				// Update the entity.
				var tracked = _dbContext.Set<$modelclass$>().Update(
					model
					);

                // Log what we are about to do.
                _logger.LogDebug(
					"Saving changes to the $efcorecontextclass$ data-context"
					);

				// Save the changes.
				await _dbContext.SaveChangesAsync(
					cancellationToken
					).ConfigureAwait(false);
	
				// Return the results
                return tracked.Entity;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to update a $modelclass$ instance in the " +
					"$efcorecontextclass$ data-context"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to update a $modelclass$ " +
					"instance in the $efcorecontextclass$ data-context!",
					innerException: ex
					);
			}
		}
		$endif$		
		#endregion
	}
}

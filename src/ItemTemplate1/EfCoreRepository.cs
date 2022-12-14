
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
        /// This field contains the EFCORE data-context factory for this repository.
        /// </summary>
		protected readonly IDbContextFactory<$efcorecontextclass$> _dbContextFactory;

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
        /// <param name="dbContextFactory">The EFCORE data-context factory
		/// to use with this repository.</param>
		/// <param name="logger">The logger to use with this repository.</param>
		public $newclassname$(
            IDbContextFactory<$efcorecontextclass$> dbContextFactory,
			ILogger<$newclassname$> logger
            )
		{
			// Save the reference(s).
			_dbContextFactory = dbContextFactory;
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
                    "Creating a $efcorecontextclass$ data-context"
                    );

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
                    "Searching for $modelclass$ instances"
                    );

				// Create the entity in the data-store.
				var data = await dbContext.Set<$modelclass$>().AnyAsync(
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
                    "Failed to search for $modelclass$ instances"
                    );

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for $modelclass$ " +
					"instances!",
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
                    "Creating a $efcorecontextclass$ data-context"
                    );

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
                    "Creating a new $modelclass$ instance in the " +
					"$efcorecontextclass$ data-context"
                    );

				// Create the entity in the data-store.
				var entity = await dbContext.Set<$modelclass$>()
					.AddAsync(
						model,
						cancellationToken
						).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
                    "Saving changes to the $efcorecontextclass$ data-context"
                    );

				// Save the changes.
				await dbContext.SaveChangesAsync(
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
                    "Failed to create a $modelclass$ instance"
                    );

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to create a $modelclass$ " +
					"instance!",
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
					"Creating a $efcorecontextclass$ data-context"
					);

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
					"Attaching a $modelclass$ instance to the $efcorecontextclass$ " +
					"data-context"
					);

				// Get a tracked instance from the data-store.
				var trackedMimeType = dbContext.Set<$modelclass$>().Attach(
					model
					);

				// Log what we are about to do.
				_logger.LogDebug(
					"Removing a $modelclass$ instance from the " +
					"$efcorecontextclass$ data-context"
					);

				// Remove the entity from the data-store.
				var entity = dbContext.Set<$modelclass$>()
					.Remove(
						trackedMimeType.Entity
						);

				// Log what we are about to do.
				_logger.LogDebug(
					"Saving changes to the $efcorecontextclass$ data-context"
					);

				// Save the changes.
				await dbContext.SaveChangesAsync(
					cancellationToken
					).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to remove a $modelclass$ instance"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to remove a $modelclass$ " +
					"instance!",
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
					"Creating a $efcorecontextclass$ data-context"
					);

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
                    "Searching for matching $modelclass$ instances from" +
					"a $efcorecontextclass$ data-context"
                    );

				// Perform the search.
				var data = await dbContext.Set<$modelclass$>().Where(expression)
					.ToListAsync(
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
					"Failed to search for matching $modelclass$ instances"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for matching " +
					$"$modelclass$ instances!",
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
					"Creating a $efcorecontextclass$ data-context"
					);

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
					"Searching for a matching $modelclass$ instance from" +
					"a $efcorecontextclass$ data-context"
					);

				// Perform the search.
				var data = await dbContext.Set<$modelclass$>().Where(
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
					"Failed to search for a matching $modelclass$ instance"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to search for a matching " +
					$"$modelclass$ instance!",
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
					"Creating a $efcorecontextclass$ data-context"
					);

				// Create a database context.
				using var dbContext = await _dbContextFactory.CreateDbContextAsync(
					cancellationToken
					).ConfigureAwait(false);

				// Log what we are about to do.
				_logger.LogDebug(
					"Attaching a $modelclass$ instance to the $efcorecontextclass$ " +
					"data-context"
					);

				// Get a tracked instance from the data-store.
				var trackedMimeType = dbContext.Set<$modelclass$>().Attach(
					model
					);

				// Log what we are about to do.
				_logger.LogDebug(
					"Updating a $modelclass$ instance from the " +
					"$efcorecontextclass$ data-context"
					);

				// Update the entity.
				dbContext.Set<$modelclass$>().Update(
					trackedMimeType.Entity
					);

                // Log what we are about to do.
                _logger.LogDebug(
					"Saving changes to the $efcorecontextclass$ data-context"
					);

				// Save the changes.
				await dbContext.SaveChangesAsync(
					cancellationToken
					).ConfigureAwait(false);
	
				// Return the results
                return trackedMimeType.Entity;
			}
			catch (Exception ex)
			{
				// Log what happened.
				_logger.LogError(
					ex,
					"Failed to update a $modelclass$ instance"
					);

				// Provider better context.
				throw new RepositoryException(
					message: $"The repository failed to update a $modelclass$ " +
					"instance!",
					innerException: ex
					);
			}
		}
		$endif$		
		#endregion
	}
}

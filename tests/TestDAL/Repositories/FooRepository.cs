
#region Local using statements
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TestDAL.EfCore;
using TestDAL.Models;

#endregion

namespace TestDAL.Repositories
{
    /// <summary>
    /// This class is an EFCORE implementation of the <see cref="IFooRepository"/>
    /// interface.
    /// </summary>
    internal class FooRepository : IFooRepository
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the EFCORE data-context factory for this repository.
        /// </summary>
        protected readonly IDbContextFactory<TestDataContext> _dbContextFactory;

        /// <summary>
        /// This field contains the logger for this repository.
        /// </summary>
        protected readonly ILogger<FooRepository> _logger;

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="FooRepository"/>
        /// class.
        /// </summary>
        /// <param name="dbContextFactory">The EFCORE data-context factory
        /// to use with this repository.</param>
        /// <param name="logger">The logger to use with this repository.</param>
        public FooRepository(
            IDbContextFactory<TestDataContext> dbContextFactory,
            ILogger<FooRepository> logger
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

        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(
            Expression<Func<Foo, bool>> expression,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a new Foo instance in the " +
                    "TestDataContext data-context"
                    );

                // Create the entity in the data-store.
                var data = await dbContext.Set<Foo>().AnyAsync(
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
                    "Failed to search for matching Foo instances in " +
                    "the TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to search for matching " +
                    "Foo instances in the TestDataContext data-context!",
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual async Task<Foo> CreateAsync(
            Foo model,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a new Foo instance in the " +
                    "TestDataContext data-context"
                    );

                // Create the entity in the data-store.
                var entity = await dbContext.Set<Foo>()
                    .AddAsync(
                        model,
                        cancellationToken
                        ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Saving changes to the TestDataContext data-context"
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
                    "Failed to create a Foo instance in the " +
                    "TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to create a Foo " +
                    "instance in the TestDataContext data-context!",
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(
            Foo model,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Attaching a Foo instance to the TestDataContext " +
                    "data-context"
                    );

                // Get a tracked instance from the data-store.
                var trackedMimeType = dbContext.Set<Foo>().Attach(
                    model
                    );

                // Log what we are about to do.
                _logger.LogDebug(
                    "Removing a Foo instance from the " +
                    "TestDataContext data-context"
                    );

                // Remove the entity from the data-store.
                var entity = dbContext.Set<Foo>()
                    .Remove(
                        trackedMimeType.Entity
                        );

                // Log what we are about to do.
                _logger.LogDebug(
                    "Saving changes to the TestDataContext data-context"
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
                    "Failed to remove a Foo instance from the " +
                    "TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to remove a Foo " +
                    "instance from the TestDataContext data-context!",
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<Foo>> FindAsync(
            Expression<Func<Foo, bool>> expression,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Searching for matching Foo instances from" +
                    "a TestDataContext data-context"
                    );

                // Perform the search.
                var data = await dbContext.Set<Foo>().Where(expression)
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
                    "Failed to search for matching Foo instances from " +
                    "the TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to search for matching " +
                    $"Foo instances from the TestDataContext " +
                    "data-context!",
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual async Task<Foo?> FindSingleAsync(
            Expression<Func<Foo, bool>> expression,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Searching for a matching Foo instance from" +
                    "a TestDataContext data-context"
                    );

                // Perform the search.
                var data = await dbContext.Set<Foo>().Where(
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
                    "Failed to search for a matching Foo instance from " +
                    "the TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to search for a matching " +
                    $"Foo instance from the TestDataContext " +
                    "data-context!",
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual async Task<Foo> UpdateAsync(
            Foo model,
            CancellationToken cancellationToken = default
            )
        {
            try
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TestDataContext data-context"
                    );

                // Create a database context.
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(
                    cancellationToken
                    ).ConfigureAwait(false);

                // Log what we are about to do.
                _logger.LogDebug(
                    "Attaching a Foo instance to the TestDataContext " +
                    "data-context"
                    );

                // Get a tracked instance from the data-store.
                var trackedMimeType = dbContext.Set<Foo>().Attach(
                    model
                    );

                // Log what we are about to do.
                _logger.LogDebug(
                    "Updating a Foo instance from the " +
                    "TestDataContext data-context"
                    );

                // Update the entity.
                dbContext.Set<Foo>().Update(
                    trackedMimeType.Entity
                    );

                // Log what we are about to do.
                _logger.LogDebug(
                    "Saving changes to the TestDataContext data-context"
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
                    "Failed to update a Foo instance in the " +
                    "TestDataContext data-context"
                    );

                // Provider better context.
                throw new RepositoryException(
                    message: $"The repository failed to update a Foo " +
                    "instance in the TestDataContext data-context!",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}

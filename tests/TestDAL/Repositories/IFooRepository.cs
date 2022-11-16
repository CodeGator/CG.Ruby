#region Local using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestDAL.Models;

#endregion

namespace TestDAL.Repositories
{
    /// <summary>
    /// This interface represents an object that manages the storage and retrieval
    /// of <see cref="Foo"/> objects.
    /// </summary>
    public interface IFooRepository
    {

        /// <summary>
        /// This method creates a new <see cref="Foo"/> object in the 
        /// underlying storage.
        /// </summary>
        /// <param name="model">The model to create in the underlying storage.</param>
        /// <param name="cancellationToken">A cancellation token that is monitored
        /// for the lifetime of the method.</param>
        /// <returns>A task to perform the operation that returns the newly created
        /// <see cref="Foo"/> object.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever one
        /// or more arguments are missing, or invalid.</exception>
        /// <exception cref="RepositoryException">This exception is thrown whenever the
        /// repository fails to complete the operation.
        Task<Foo> CreateAsync(
            Foo model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes an existing <see cref="Foo"/> object from the 
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
            Foo model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method searches for matching <see cref="Foo"/> objects using
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
        Task<IEnumerable<Foo>> FindAsync(
            Expression<Func<Foo, bool>> expression,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method searches for a single matching <see cref="Foo"/> object using
        /// the given LINQ expression.
        /// </summary>
        /// <param name="expression">The LINQ expression to use for the search.</param>
        /// <param name="cancellationToken">A cancellation token that is monitored
        /// for the lifetime of the method.</param>
        /// <returns>A task to perform the operation that returns a matching <see cref="Foo"/> 
        /// object, if one was found, or NULL otherwise.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever one
        /// or more arguments are missing, or invalid.</exception>
        /// <exception cref="RepositoryException">This exception is thrown whenever the
        /// repository fails to complete the operation.
        Task<Foo?> FindSingleAsync(
            Expression<Func<Foo, bool>> expression,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates an existing <see cref="Foo"/> object in the 
        /// underlying storage.
        /// </summary>
        /// <param name="model">The model to update in the underlying storage.</param>
        /// <param name="cancellationToken">A cancellation token that is monitored
        /// for the lifetime of the method.</param>
        /// <returns>A task to perform the operation that returns the newly updated
        /// <see cref="Foo"/> object.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever one
        /// or more arguments are missing, or invalid.</exception>
        /// <exception cref="RepositoryException">This exception is thrown whenever the
        /// repository fails to complete the operation.
        Task<Foo> UpdateAsync(
            Foo model,
            CancellationToken cancellationToken = default
            );

    }
}

using LendingLibrary.Core.Domain.Entities;
using System.Linq.Expressions;

namespace LendingLibrary.Core.Domain.RepositoryInterfaces;

public interface IBooksRepository
{
    Task<List<Book>> GetAllBooks();

    Task<List<Book>> GetFilteredBooks(Expression<Func<Book,bool>> predicate);

    Task<Book?> GetBookById(Guid id);

    Task<bool> ChangeBookStatus(string status, Guid bookId);

    Task<Book?> GetBookByIdByStatus(Guid bookId, string status);
}

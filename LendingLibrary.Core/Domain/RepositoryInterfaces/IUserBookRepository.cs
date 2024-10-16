using LendingLibrary.Core.Domain.Entities;

namespace LendingLibrary.Core.Domain.RepositoryInterfaces;

public interface IUserBookRepository
{
    Task AddUserBook(UserBook userBook);

    Task<bool> UserBookExists(Guid userId, Guid bookId);

    Task<List<UserBook>> GetAllUserBooks(Guid userId);
}

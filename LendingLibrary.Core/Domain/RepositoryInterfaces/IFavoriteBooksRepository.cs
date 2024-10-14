using LendingLibrary.Core.Domain.Entities;

namespace LendingLibrary.Core.Domain.RepositoryInterfaces;

public interface IFavoriteBooksRepository
{
    Task<bool> FavoriteBookExists(Guid userId, Guid bookId);
    Task AddToFavorite(FavoriteBook favoriteBook);

    Task<FavoriteBook?> GetFavoriteBookById(Guid favoriteId);

    Task<List<FavoriteBook>> GetAllFavoriteBooks(Guid userId);

    Task<bool> DeleteFavoriteBook(Guid favoriteId);
}

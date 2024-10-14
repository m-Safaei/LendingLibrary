using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.FavoriteBook;
using LendingLibrary.Core.DTO.UserRequest;

namespace LendingLibrary.Core.ServiceInterfaces;

public interface IFavoriteBooksService
{
    Task<bool> AddToFavorite(Guid userId, Guid bookId);

    Task<FavoriteBook> GetFavoriteBookById(Guid favoriteId);

    Task<List<FavoriteBook>> GetAllFavoriteBooks(Guid userId);

    Task<bool> DeleteFavoriteBook(Guid favoriteId);

    Task<List<FavoriteBookDto>> GetFavoriteBooks(Guid userId);
}

using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.DTO.FavoriteBook;
using LendingLibrary.Core.ServiceInterfaces;

namespace LendingLibrary.Core.Services;

public class FavoriteBooksService : IFavoriteBooksService
{
    private readonly IFavoriteBooksRepository _favoriteBooksRepository;
    private readonly IBooksService _booksService;

    public FavoriteBooksService(IFavoriteBooksRepository favoriteBooksRepository
        ,IBooksService booksService)
    {
        _favoriteBooksRepository = favoriteBooksRepository;
        _booksService = booksService;
    }

    public async Task<bool> AddToFavorite(Guid userId, Guid bookId)
    {
        if (await _favoriteBooksRepository.FavoriteBookExists(userId, bookId)) 
        {
            return false;
        }

        FavoriteBook favorite = new()
        {
            ApplicationUserId = userId,
            BookId = bookId
        };
        
        await _favoriteBooksRepository.AddToFavorite(favorite);
        return true;
    }

    public async Task<bool> DeleteFavoriteBook(Guid favoriteId)
    {
        return await _favoriteBooksRepository.DeleteFavoriteBook(favoriteId);
    }

    public async Task<List<FavoriteBook>> GetAllFavoriteBooks(Guid userId)
    {
        return await _favoriteBooksRepository.GetAllFavoriteBooks(userId);
    }

    public async Task<FavoriteBook> GetFavoriteBookById(Guid favoriteId)
    {
        FavoriteBook? favorite = await _favoriteBooksRepository.GetFavoriteBookById(favoriteId);
        if (favorite == null) return null;
        return favorite;
    }

    public async Task<List<FavoriteBookDto>> GetFavoriteBooks(Guid userId)
    {
        List<FavoriteBook> favorites = await GetAllFavoriteBooks(userId);

        List<Guid> bookIds = favorites.Select(f => f.BookId).ToList();

        List<BookResponseDto> books = new();
        foreach(Guid bookId in bookIds)
        {
            books.Add(await _booksService.GetBookById(bookId));
        }

        List<FavoriteBookDto> result = new();

        foreach(BookResponseDto book in books)
        {
            foreach(FavoriteBook favorite in favorites)
            {
                if(favorite.BookId == book.Id)
                {
                    if (result.Any(f => f.FavoriteId == favorite.Id)) break;
                    result.Add(new FavoriteBookDto()
                    {
                        FavoriteId = favorite.Id,
                        BookId = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Language = book.Language,
                        PublishedYear = book.PublishedYear,
                        Status = book.Status,
                    });
                }
            }
        }
        return result;
    }
}

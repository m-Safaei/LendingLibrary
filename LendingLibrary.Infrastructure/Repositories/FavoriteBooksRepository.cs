using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace LendingLibrary.Infrastructure.Repositories;

public class FavoriteBooksRepository : IFavoriteBooksRepository
{
    private readonly LendingLibraryDbContext _context;

    public FavoriteBooksRepository(LendingLibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddToFavorite(FavoriteBook favoriteBook)
    {
        await _context.FavoriteBooks.AddAsync(favoriteBook);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteFavoriteBook(Guid favoriteId)
    {
        FavoriteBook? favorite = await GetFavoriteBookById(favoriteId);
        if (favorite == null) return false;

        _context.FavoriteBooks.Remove(favorite);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> FavoriteBookExists(Guid userId, Guid bookId)
    {
        return await _context.FavoriteBooks
                             .AnyAsync(f => f.ApplicationUserId == userId && f.BookId == bookId);
    }

    public async Task<List<FavoriteBook>> GetAllFavoriteBooks(Guid userId)
    {
       return await _context.FavoriteBooks.Where(f=>f.ApplicationUserId ==userId).ToListAsync();
    }

    public async Task<FavoriteBook?> GetFavoriteBookById(Guid favoriteId)
    {
        return await _context.FavoriteBooks.SingleOrDefaultAsync(f => f.Id == favoriteId);
    }
}

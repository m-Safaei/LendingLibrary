using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace LendingLibrary.Infrastructure.Repositories;

public class UserBookRepository : IUserBookRepository
{
    private readonly LendingLibraryDbContext _context;

    public UserBookRepository(LendingLibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddUserBook(UserBook userBook)
    {
        await _context.UserBooks.AddAsync(userBook);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserBookExists(Guid userId, Guid bookId)
    {
        return await _context.UserBooks.AnyAsync(b =>b.BookId == bookId);
    }
}

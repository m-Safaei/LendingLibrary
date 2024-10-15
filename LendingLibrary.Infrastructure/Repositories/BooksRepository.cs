using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LendingLibrary.Infrastructure.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly LendingLibraryDbContext _context;

    public BooksRepository(LendingLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookById(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(b=>b.Id == id);
    }

    public async Task<List<Book>> GetFilteredBooks(Expression<Func<Book, bool>> predicate)
    {
        return await _context.Books.Where(predicate).ToListAsync();
    }

    public async Task<bool> ChangeBookStatus(string status,Guid bookId)
    {
        Book? book = await GetBookById(bookId);
        if (book == null) return false;
        book.Status = status;
        book.ReturnDate = DateTime.Now.AddDays(10);
        await _context.SaveChangesAsync();
        return true;
    }
}

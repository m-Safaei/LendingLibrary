using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.Mappers;
using LendingLibrary.Core.ServiceInterfaces;

namespace LendingLibrary.Core.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;

    public BooksService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task ChangeBookStatus(string status, Guid bookId)
    {
        await _booksRepository.ChangeBookStatus(status, bookId);
    }
    public async Task<List<BookResponseDto>> GetAllBooks()
    {
        List<Book> books = await _booksRepository.GetAllBooks();

        return books.Select(b => b.ToBookResponseDto()).ToList();
    }

    public async Task<BookResponseDto?> GetBookById(Guid id)
    {
        Book? book = await _booksRepository.GetBookById(id);

        if (book == null) return null;

        return book.ToBookResponseDto();
    }

    public async Task<BookResponseDto?> GetBookByIdByStatus(Guid bookId,string status)
    {
        Book? book = await _booksRepository.GetBookByIdByStatus(bookId, status);
        if (book == null) return null;

        return book.ToBookResponseDto();
    }

    public async Task<List<BookResponseDto>> GetFilteredBooks(string searchBy, string searchString)
    {
        List<Book> books = searchBy switch
        {
            nameof(BookResponseDto.Title) =>
                await _booksRepository.GetFilteredBooks(b => b.Title.Contains(searchString)),

            nameof(BookResponseDto.Author) =>
                await _booksRepository.GetFilteredBooks(b => b.Author.Contains(searchString)),

            nameof(BookResponseDto.Language) =>
                await _booksRepository.GetFilteredBooks(b => b.Language.Contains(searchString)),

            _ => new()
        };

        return books.Select(b => b.ToBookResponseDto()).ToList();
    }
}

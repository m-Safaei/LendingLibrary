using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.Book;

namespace LendingLibrary.Core.ServiceInterfaces;

public interface IBooksService
{
    Task<BookResponseDto?> GetBookById(Guid id);

    Task<List<BookResponseDto>> GetAllBooks();

    Task<List<BookResponseDto>> GetFilteredBooks(string searchBy, string searchString);

    Task ChangeBookStatus(string status, Guid bookId);
}

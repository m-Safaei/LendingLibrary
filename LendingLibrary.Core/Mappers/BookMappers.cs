using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.Book;

namespace LendingLibrary.Core.Mappers;

public static class BookMappers
{
    public static BookResponseDto ToBookResponseDto(this Book book)
    {
        return new BookResponseDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Language = book.Language,
            PublishedYear = book.PublishedYear,
            Status = book.Status,
            ReturnDate = book.ReturnDate
        };
    }
}

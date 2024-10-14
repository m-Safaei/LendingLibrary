using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.Book;

public class BookResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Language { get; set; }

    public int PublishedYear { get; set; }

    public string? Status { get; set; }

    public DateTime ReturnDate { get; set; }
}

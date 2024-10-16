using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.Book;

public class CreateBookDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Author { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string Language { get; set; }

    public int PublishedYear { get; set; }

}

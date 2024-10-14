namespace LendingLibrary.Core.DTO.FavoriteBook;

public class FavoriteBookDto
{
    public Guid FavoriteId { get; set; }
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Language { get; set; }

    public int PublishedYear { get; set; }

    public string? Status { get; set; }
}

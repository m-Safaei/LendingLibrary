namespace LendingLibrary.Core.DTO.UserRequest;

public class UserRequestResponseDto
{
    public Guid RequestId { get; set; }
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Language { get; set; }

    public int PublishedYear { get; set; }

    public string? Status { get; set; }

    public string Request {  get; set; }
}

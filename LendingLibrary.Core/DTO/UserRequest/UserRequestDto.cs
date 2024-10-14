using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.UserRequest;

public class UserRequestDto
{
    public Guid Id { get; set; }

    public string Request { get; set; }

    public Guid ApplicationUserId { get; set; }

    public Guid BookId { get; set; }
}

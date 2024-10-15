namespace LendingLibrary.Core.DTO.UserRequest;

public class UsersRequestsAdminSideDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public List<UserRequestResponseDto> requests { get; set; }
}

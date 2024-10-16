using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.DTO.UserRequest;

namespace LendingLibrary.Core.ServiceInterfaces;

public interface IUserRequestsService
{
    Task<bool> AddRequest(Guid userId, Guid bookId, string request);

    Task<UserRequestDto?> GetUserRequestById(Guid requestId);

    Task<List<UserRequestDto>> GetAllUserRequests(Guid userId);

    Task<bool> DeleteRequest(Guid requestId);

    Task<List<UserRequestResponseDto>> GetBooks(Guid userId);

    Task<List<UsersRequestsAdminSideDto>> GetAllRequests();

    Task<bool> ConfirmRequest(string request, Guid bookId, Guid userId);

    Task<List<BookResponseDto>> GetUserBooks(string status, Guid userId);
}

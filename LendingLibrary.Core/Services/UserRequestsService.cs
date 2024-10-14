using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Core.DTO.UserRequest;
using LendingLibrary.Core.ServiceInterfaces;

namespace LendingLibrary.Core.Services;

public class UserRequestsService : IUserRequestsService
{
    private readonly IUserRequestsRepository _userRequestsRepository;

    public UserRequestsService(IUserRequestsRepository userRequestsRepository)
    {
        _userRequestsRepository = userRequestsRepository;
    }

    public async Task AddRequest(Guid userId, Guid bookId, string request)
    {
        UserRequest userRequest = new()
        {
            BookId = bookId,
            ApplicationUserId = userId,
            Request = request
        };

        await _userRequestsRepository.AddRequest(userRequest);
    }

    public async Task<bool> DeleteRequest(Guid requestId)
    {
        return await _userRequestsRepository.DeleteRequest(requestId);
    }

    public async Task<List<UserRequestDto>> GetAllUserRequests(Guid userId)
    {
        List<UserRequest> requests = await _userRequestsRepository.GetAllUserRequests(userId);

        return requests.Select(r => new UserRequestDto()
        {
            BookId = r.BookId,
            ApplicationUserId = r.ApplicationUserId,
            Request = r.Request,
            Id = r.Id
        }).ToList();
    }

    public async Task<UserRequestDto?> GetUserRequestById(Guid requestId)
    {
        UserRequest? request = await _userRequestsRepository.GetUserRequestById(requestId);
        if(request == null) return null;

        UserRequestDto userRequestDto = new()
        {
            BookId = request.BookId,
            ApplicationUserId = request.ApplicationUserId,
            Request = request.Request,
            Id = request.Id
        };
        return userRequestDto;
    }
}

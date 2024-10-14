using LendingLibrary.Core.Domain.Entities;

namespace LendingLibrary.Core.Domain.RepositoryInterfaces;

public interface IUserRequestsRepository
{
    Task AddRequest(UserRequest userRequest);

    Task<UserRequest?> GetUserRequestById(Guid requestId);

    Task<List<UserRequest>> GetAllUserRequests(Guid userId);

    Task<bool> DeleteRequest(Guid requestId);
}

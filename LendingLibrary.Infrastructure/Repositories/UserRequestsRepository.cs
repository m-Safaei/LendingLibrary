using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace LendingLibrary.Infrastructure.Repositories;

public class UserRequestsRepository : IUserRequestsRepository
{
    private readonly LendingLibraryDbContext _context;

    public UserRequestsRepository(LendingLibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddRequest(UserRequest userRequest)
    {
        await _context.UserRequests.AddAsync(userRequest);
        await _context.SaveChangesAsync();
    }


    public async Task<List<UserRequest>> GetAllUserRequests(Guid userId)
    {
        return await _context.UserRequests.Where(r => r.ApplicationUserId == userId)
                                          .ToListAsync();
    }

    public async Task<UserRequest?> GetUserRequestById(Guid requestId)
    {
        return await _context.UserRequests.SingleOrDefaultAsync(r => r.Id == requestId);
    }
    public async Task<bool> DeleteRequest(Guid requestId)
    {
        UserRequest? request = await GetUserRequestById(requestId);

        if(request == null) return false;

        _context.UserRequests.Remove(request);
        await _context.SaveChangesAsync();
        return true;
    }
}

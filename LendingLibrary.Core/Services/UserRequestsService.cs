using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.DTO.UserRequest;
using LendingLibrary.Core.ServiceInterfaces;

namespace LendingLibrary.Core.Services;

public class UserRequestsService : IUserRequestsService
{
    private readonly IUserRequestsRepository _userRequestsRepository;
    private readonly IBooksService _booksService;
    public UserRequestsService(IUserRequestsRepository userRequestsRepository, IBooksService booksService)
    {
        _userRequestsRepository = userRequestsRepository;
        _booksService = booksService;
    }

    public async Task<bool> AddRequest(Guid userId, Guid bookId, string request)
    {
        if (await _userRequestsRepository.RequestExists(userId, bookId)) 
        {
            return false;
        }
        UserRequest userRequest = new()
        {
            BookId = bookId,
            ApplicationUserId = userId,
            Request = request
        };

        await _userRequestsRepository.AddRequest(userRequest);
        return true;
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

    public async Task<List<UserRequestResponseDto>> GetBooks(Guid userId)
    {
        List<UserRequest> requests = await _userRequestsRepository.GetAllUserRequests(userId);

        List<Guid> bookIds = requests.Select(b => b.BookId).ToList();

        List<BookResponseDto> books = new();

        foreach (var bookId in bookIds)
        {
            books.Add(await _booksService.GetBookById(bookId));
        }

        List<UserRequestResponseDto> result = new();
        foreach(var book in books)
        {
            foreach(var request in requests)
            {
                if(request.BookId == book.Id)
                {
                    if (result.Any(r => r.RequestId == request.Id)) break;
                    result.Add(new UserRequestResponseDto()
                    {
                        RequestId = request.Id,
                        BookId = book.Id,
                        Request = request.Request,
                        Title = book.Title,
                        Author = book.Author,
                        Language = book.Language,
                        PublishedYear = book.PublishedYear,
                        Status = book.Status,
                    });
                }
            }
        }

        return result;
    }

    public async Task<UserRequestDto?> GetUserRequestById(Guid requestId)
    {
        UserRequest? request = await _userRequestsRepository.GetUserRequestById(requestId);
        if (request == null) return null;

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

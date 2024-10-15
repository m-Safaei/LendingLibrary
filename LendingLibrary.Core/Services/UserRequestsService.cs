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
    private readonly IUserBookRepository _userBookRepository;

    public UserRequestsService(IUserRequestsRepository userRequestsRepository, 
                     IBooksService booksService
                    ,IUserBookRepository userBookRepository)
    {
        _userRequestsRepository = userRequestsRepository;
        _booksService = booksService;
        _userBookRepository = userBookRepository;
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
                        RequestDate = request.RequestDate
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

    public async Task<List<UsersRequestsAdminSideDto>> GetAllRequests()
    {
        List<UserRequest> userRequests = await _userRequestsRepository.GetAllRequests();

        List<Guid> userIds = userRequests.Select(r=>r.ApplicationUserId).ToList();

        List<UsersRequestsAdminSideDto> userRequestsAdminSideDto = new();

        foreach (Guid userId in userIds) 
        { 
            string userName = _userRequestsRepository.GetUserNameById(userId);
            List<UserRequestResponseDto> requests = await GetBooks(userId);

            userRequestsAdminSideDto.Add(new UsersRequestsAdminSideDto()
            {
                UserId = userId,
                UserName = userName,
                requests = requests
            });
        }
        return userRequestsAdminSideDto;
    }
    public async Task<bool> ConfirmRequest(string request, Guid bookId, Guid userId)
    {
        if (await _userBookRepository.UserBookExists(userId, bookId)) return false;

        if(request == "امانت")
        {
            await _booksService.ChangeBookStatus("در امانت", bookId);
        }
        else if(request == "رزرو")
        {
            await _booksService.ChangeBookStatus("رزرو شده", bookId);
        }

        UserBook userBook = new()
        {
            BookId = bookId,
            ApplicationUserId = userId,
        };
        await _userBookRepository.AddUserBook(userBook);
        return true;
    }
}

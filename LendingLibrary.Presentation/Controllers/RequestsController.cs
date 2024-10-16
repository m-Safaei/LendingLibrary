using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.DTO.UserRequest;
using LendingLibrary.Core.ExtensionMethods;
using LendingLibrary.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class RequestsController : Controller
{
    #region ctor
    private readonly IUserRequestsService _userRequestsService;

    public RequestsController(IUserRequestsService userRequestsService)
    {
        _userRequestsService = userRequestsService;
    }
    #endregion


    public async Task<IActionResult> SendRequest(Guid bookId, string request)
    {
        Guid userId = User.GetUserId();
        bool res = await _userRequestsService.AddRequest(userId, bookId, request);
        if (!res)
        {
            TempData["RequestError"] = "درخواست تکراری است";
            return RedirectToAction("UserProfile", "User");
        }

        return RedirectToAction("ListOfRequests", "Requests");
    }

    public async Task<IActionResult> ListOfRequests()
    {
        Guid userId = User.GetUserId();

        List<UserRequestResponseDto> requests = await _userRequestsService.GetBooks(userId);

        return View(requests);
    }

    public async Task<IActionResult> DeleteRequest(Guid id)
    {
        bool res = await _userRequestsService.DeleteRequest(id);
        if (res)
        {
            TempData["SuccessMessage"] = "عملیات باموفقیت انجام شد";
        }
        else
        {
            TempData["ErrorMessage"] = "عملیات ناموفق";
        }
        return RedirectToAction("ListOfRequests", "Requests");
    }

    public async Task<IActionResult> GetUserBooks(string status)
    {
        Guid userId = User.GetUserId();

        List<BookResponseDto> books = await _userRequestsService.GetUserBooks(status, userId);
        return View(books);
    }
}

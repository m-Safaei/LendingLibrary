using LendingLibrary.Core.DTO.UserRequest;
using LendingLibrary.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RequestsController : Controller
{
    private readonly IUserRequestsService _userRequestsService;

    public RequestsController(IUserRequestsService userRequestsService)
    {
        _userRequestsService = userRequestsService;
    }

    public async Task<IActionResult> ListOfRequests()
    {
        List<UsersRequestsAdminSideDto> requests = await _userRequestsService.GetAllRequests();
        return View(requests);
    }

    public async Task<IActionResult> ConfirmRequest(string request, Guid bookId, Guid userId)
    {
        bool result = await _userRequestsService.ConfirmRequest(request, bookId, userId);

        if (!result)
        {
            TempData["ErrorForConfirmRequest"] = "درخواست قبلا تایید شده";

            return RedirectToAction("ListOfRequests", "Requests", new {area="Admin"});
        }

        TempData["InfoForConfirmRequest"] = "درخواست تایید شد";

        return RedirectToAction("BookPage", "Books", new { id = bookId });
    }

    public async Task<IActionResult> DeleteRequest(Guid requestId)
    {
        bool res = await _userRequestsService.DeleteRequest(requestId);
        if (res)
        {
            TempData["SuccessMessage"] = "عملیات باموفقیت انجام شد";
        }
        else
        {
            TempData["ErrorMessage"] = "عملیات ناموفق";
        }
        return RedirectToAction("ListOfRequests", "Requests", new { area = "Admin" });
    }

}

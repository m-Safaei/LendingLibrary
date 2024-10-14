using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class RequestsController : Controller
{
   
    public async Task<IActionResult> SendRequest(Guid bookId,string request)
    {

        return View();
    }
}

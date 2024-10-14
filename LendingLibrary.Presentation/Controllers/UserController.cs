using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    public IActionResult UserProfile()
    {
        return View();
    }
}

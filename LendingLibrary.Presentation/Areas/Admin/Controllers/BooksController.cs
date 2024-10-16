using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Areas.Admin.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class BooksController : Controller
{
    public IActionResult CreatBook()
    {
        return View();
    }


}

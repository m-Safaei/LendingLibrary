using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class FavoriteBooksController : Controller
{
    public IActionResult ListOfFavorites()
    {
        return View();
    }
}

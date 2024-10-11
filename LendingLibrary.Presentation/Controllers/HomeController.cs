using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

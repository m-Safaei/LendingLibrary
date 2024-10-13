using LendingLibrary.Core.DTO.Book;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Route("[controller]/[action]")]
public class BooksController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Search(string searchBy,string? searchString)
    {
        ViewBag.SearchFields = new Dictionary<string, string>() {
            {nameof(BookResponseDto.Title), "عنوان" },
            {nameof(BookResponseDto.Author), "نویسنده" },
            {nameof(BookResponseDto.Language),"زبان" }
        };

        if(string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(searchBy))
        {
            return View();
        }

        return View();
    }
}

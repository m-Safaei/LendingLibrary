using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Route("[controller]/[action]")]
public class BooksController : Controller
{
    #region ctor

    private readonly IBooksService _booksService;
    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }
    #endregion

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

        List<BookResponseDto> books = await _booksService.GetFilteredBooks(searchBy, searchString);

        ViewBag.SearchBy = searchBy;
        ViewBag.SearchString = searchString;

        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> BookPage(Guid id)
    {
        BookResponseDto? book = await _booksService.GetBookById(id);
        if(book == null)
        {
            return RedirectToAction("Search", "Books");
        }

        return View(book);
    }
}

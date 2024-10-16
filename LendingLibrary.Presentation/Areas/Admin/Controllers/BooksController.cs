using LendingLibrary.Core.DTO.Book;
using LendingLibrary.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    public async Task<IActionResult> GetListOfBooks()
    {
        List<BookResponseDto> books = await _booksService.GetAllBooks();
        return View(books);
    }

    [HttpGet]
    public IActionResult CreateBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto bookDto)
    {
        if(!ModelState.IsValid) return View(bookDto);

        bool result = await _booksService.CreateBook(bookDto);

        if(!result)
        {
            TempData["ErrorForCreateBook"] = "عنوان کتاب تکراری است";
            return View(bookDto);
        }

        return RedirectToAction(nameof(GetListOfBooks));
    }
}

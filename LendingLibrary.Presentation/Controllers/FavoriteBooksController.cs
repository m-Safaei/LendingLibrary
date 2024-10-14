using LendingLibrary.Core.DTO.FavoriteBook;
using LendingLibrary.Core.ExtensionMethods;
using LendingLibrary.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class FavoriteBooksController : Controller
{
    private readonly IFavoriteBooksService _favoriteBooksService;

    public FavoriteBooksController(IFavoriteBooksService favoriteBooksService)
    {
        _favoriteBooksService = favoriteBooksService;
    }

    public async Task<IActionResult> AddBookToFavorites(Guid bookId)
    {
        Guid userId = User.GetUserId();
        
        bool res = await _favoriteBooksService.AddToFavorite(userId, bookId);
        if (!res)
        {
            TempData["RequestError"] = "کتاب مورد‌نظر در لیست علاقه‌مندی‌ها موجود است";
            return RedirectToAction("ListOfFavorites", "FavoriteBooks");
        }
        return RedirectToAction("ListOfFavorites", "FavoriteBooks");
    }

    public async Task<IActionResult> ListOfFavorites()
    {
        Guid userId = User.GetUserId();

        List<FavoriteBookDto> favorites = await _favoriteBooksService.GetFavoriteBooks(userId);

        return View(favorites);
    }

    public async Task<IActionResult> DeleteFavoriteBook(Guid favoriteId)
    {
        bool res = await _favoriteBooksService.DeleteFavoriteBook(favoriteId);
        if (res)
        {
            TempData["SuccessMessage"] = "عملیات باموفقیت انجام شد";
        }
        else
        {
            TempData["ErrorMessage"] = "عملیات ناموفق";
        }
        return RedirectToAction("ListOfFavorites", "FavoriteBooks");
    }
}

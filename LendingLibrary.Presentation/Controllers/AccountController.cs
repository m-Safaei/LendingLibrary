using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendingLibrary.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
    #region ctor
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    #endregion


    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(registerDto);
        }

        ApplicationUser user = new()
        {
            Email = registerDto.Email,
            PhoneNumber = registerDto.Mobile,
            UserName = registerDto.Email
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            if(await _roleManager.FindByNameAsync("User") != null)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            //Sign in:
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");

        }
        foreach (var error in result.Errors)
        {

            ModelState.AddModelError("Register", error.Description);
        }
        return View(registerDto);
    }

    public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Json(true); //Valid
        }

        return Json(false);//invalid
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index","Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto, string? ReturnUrl)
    {
        if (!ModelState.IsValid) return View(loginDto);

        var result = await _signInManager.PasswordSignInAsync(loginDto.Email,
                           loginDto.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("Login", "ایمیل یا پسورد نادرست است");

        return View(loginDto);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

}
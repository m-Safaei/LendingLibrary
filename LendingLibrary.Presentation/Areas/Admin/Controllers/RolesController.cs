using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.DTO.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LendingLibrary.Presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RolesController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IActionResult> ListOfRoles()
    {
        List<ApplicationRole> roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    public IActionResult CreateRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(RoleDto roleDto)
    {
        if(!ModelState.IsValid) return View(roleDto);

        if(await _roleManager.FindByNameAsync(roleDto.Name) is null)
        {
            ApplicationRole applicationRole = new()
            {
                Name = roleDto.Name,
            };

            await _roleManager.CreateAsync(applicationRole);

            return RedirectToAction(nameof(ListOfRoles));
        }

        TempData["CreateRoleError"] = "نقش وارد شده تکراری است";
        return View(roleDto);
    }
}

using LendingLibrary.Core.Domain.Entities;
using LendingLibrary.Core.Domain.RepositoryInterfaces;
using LendingLibrary.Core.ServiceInterfaces;
using LendingLibrary.Core.Services;
using LendingLibrary.Infrastructure.AppDbContext;
using LendingLibrary.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<LendingLibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));

//Enable Identity:
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredUniqueChars = 3;
    })
    .AddEntityFrameworkStores<LendingLibraryDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, LendingLibraryDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, LendingLibraryDbContext, Guid>>();

builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IUserRequestsRepository, UserRequestsRepository>();
builder.Services.AddScoped<IUserRequestsService, UserRequestsService>();
builder.Services.AddScoped<IFavoriteBooksRepository, FavoriteBooksRepository>();
builder.Services.AddScoped<IFavoriteBooksService, FavoriteBooksService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

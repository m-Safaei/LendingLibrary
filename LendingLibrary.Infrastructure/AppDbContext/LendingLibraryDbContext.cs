using LendingLibrary.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LendingLibrary.Infrastructure.AppDbContext;

public class LendingLibraryDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public LendingLibraryDbContext(DbContextOptions<LendingLibraryDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
}


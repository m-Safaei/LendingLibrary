using System.ComponentModel.DataAnnotations;
using LendingLibrary.Core.Enums;

namespace LendingLibrary.Core.Domain.Entities;

public class Book
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Author { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string Language { get; set; }

    public int PublishedYear { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string? Status { get; set; }

    public DateTime ReturnDate { get; set; }


}


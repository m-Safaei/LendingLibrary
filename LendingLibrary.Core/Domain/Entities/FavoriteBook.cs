using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.Domain.Entities;

public class FavoriteBook
{
    [Key]
    public Guid Id { get; set; }

    public Guid ApplicationUserId { get; set; }

    public Guid BookId { get; set; }

    #region Navigation Properties

    public ApplicationUser User { get; set; }

    public Book Book { get; set; }

    #endregion
}

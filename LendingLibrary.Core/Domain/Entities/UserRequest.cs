using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LendingLibrary.Core.Domain.Entities;

public class UserRequest
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Request {  get; set; }

    public Guid ApplicationUserId { get; set; }

    public Guid BookId { get; set; }

    #region Navigation Properties

    public ApplicationUser User { get; set; }

    public Book Book { get; set; }

    #endregion
}

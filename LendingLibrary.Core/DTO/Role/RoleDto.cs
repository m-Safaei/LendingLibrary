using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.Role;

public class RoleDto
{
    [StringLength(50, MinimumLength = 3)]

    public string Name { get; set; }
}

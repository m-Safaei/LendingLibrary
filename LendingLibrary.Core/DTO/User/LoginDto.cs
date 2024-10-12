using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.User;

public class LoginDto
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
    [Required(ErrorMessage = "فیلد ایمیل اجباری است")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}

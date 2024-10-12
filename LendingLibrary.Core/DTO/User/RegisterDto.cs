using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Core.DTO.User;

public class RegisterDto
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
    [Required(ErrorMessage = "فیلد ایمیل اجباری است")]
    [Remote(action: "IsEmailAlreadyRegistered",controller: "Account",ErrorMessage =" ایمیل قبلا انتخاب شده‌است ")]
    public string Email { get; set; }

    [RegularExpression("^[0-9]*$")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل معتبر نیست")]
    public string Mobile { get; set; }

    [StringLength(50, MinimumLength = 5, ErrorMessage = "پسورد باید حداقل 5 کاراکتر داشته باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "کلمه‌ی عبور و تکرار آن یکسان نیستند")]
    public string RePassword { get; set; }
}


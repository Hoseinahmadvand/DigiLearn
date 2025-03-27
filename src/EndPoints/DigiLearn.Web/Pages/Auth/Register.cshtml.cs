using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth;

[BindProperties]
public class RegisterModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public RegisterModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    #region Properties

    [DisplayName("شماره موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [MinLength(11, ErrorMessage = "شماره موبایل نامعتبر میباشد.")]
    [MaxLength(11, ErrorMessage = "شماره موبایل نامعتبر میباشد.")]
    public string PhoneNumber { get; set; }
    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [MinLength(6, ErrorMessage = "{0} نباید کمتر از 6 کارکتر باشد !")]
    public string Password { get; set; }
    [DisplayName("تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نیست.")]
    public string ConfrimPassword { get; set; }

    #endregion

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.RegisterUser(new RegisterUserCommand()
        {
            PhoneNumber = PhoneNumber,
            Password = Password
        });
        if (result.Status == OperationResultStatus.Success)
        {
            result.Message = "ثبت نام با موفقیت انجام شد";
        }
        return RedirectAndShowAlert(result, RedirectToPage("Login"));
    }

}

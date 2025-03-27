using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UserModule.Core.Services;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Common.Application.SecurityUtil;
using DigiLearn.Web.Infrastructure.JwtUtil;

namespace DigiLearn.Web.Pages.Auth;
[BindProperties]
public class LoginModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    private readonly IConfiguration _configuration;
    public LoginModel(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
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

    public bool IsRememberMe { get; set; }
    #endregion

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnGetLogOut()
    {

        HttpContext.Response.Cookies.Delete("digi-token");
        return RedirectToPage("../Index");
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userFacade.GetUserByPhoneNumber(PhoneNumber);
        if (user == null)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }
        var isComparePassword = Sha256Hasher.IsCompare(user.Password, Password);
        if (isComparePassword == false)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        if (IsRememberMe)
        {
            HttpContext.Response.Cookies.Append("digi-token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(30),
                Secure = true
            });
        }
        else
        {
            HttpContext.Response.Cookies.Append("digi-token", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true
            });
        }

        return RedirectToPage("../Index");
    }
}

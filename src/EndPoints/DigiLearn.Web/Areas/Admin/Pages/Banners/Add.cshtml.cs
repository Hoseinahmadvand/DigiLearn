using BannerModule.Domain;
using BannerModule.Services;
using BannerModule.Services.DTOs.Command;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Areas.Admin.Pages.Banners;

[BindProperties]
public class AddModel : BaseRazor
{
    private readonly IBannerService _bannerService;

    public AddModel(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }
    [Display(Name = "تصویر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile ImageFile { get; set; }
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Url { get; set; }
    [Display(Name = " عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Alt { get; set; }

    [Display(Name = "ترتیب")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Order { get; set; }
    [Display(Name = "موقعبت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public BannerPositon Positon { get; set; }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost()
    {
        var result = await _bannerService.CreateBanner(new CreateBannerCommand()
        {
            ImageName = ImageFile,
            IsActive = true,
            Url = Url,
            Order = Order,
            Positon = Positon,
            Title = Title,
            Alt = Alt
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

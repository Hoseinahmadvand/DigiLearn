using BannerModule.Domain;
using BannerModule.Services;
using BannerModule.Services.DTOs.Command;
using CoreModule.Domain.Courses.Models;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Areas.Admin.Pages.Banners;


public class EditModel : BaseRazor
{
    private readonly IBannerService _bannerService;

    public EditModel(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    [BindProperty]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [BindProperty]
    [Display(Name = "تصویر")]
    public IFormFile? ImageFile { get; set; }

    [BindProperty]
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Url { get; set; }

    [BindProperty]
    [Display(Name = " عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Alt { get; set; }

    [BindProperty]
    [Display(Name = "ترتیب")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Order { get; set; }

    [BindProperty]
    [Display(Name = "موقعبت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public BannerPositon Positon { get; set; }

    [BindProperty]
    [Display(Name = "وضعیت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
    public async Task<IActionResult> OnGet(Guid bannerId)
    {
        var banner = await _bannerService.GetBannerById(bannerId);
        if (banner == null)
            return RedirectToPage("../Index", new { bannerId });
        Title = banner.Title;
        IsActive = banner.IsActive;
        ImageName = banner.ImageName;
        Alt = banner.Alt;
        Order = banner.Order;
        Positon = banner.Positon;
        Url = banner.Url;
        return Page();
    }
    public async Task<IActionResult> OnPost(Guid bannerId)
    {
        
        var result = await _bannerService.EditBanner(new EditBannerCommand()
        {
            Id = bannerId,
            Title = Title,
            IsActive = IsActive,
            ImageName = ImageFile,
            Alt = Alt,
            Order = Order,
            Positon = Positon,
            Url = Url,
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

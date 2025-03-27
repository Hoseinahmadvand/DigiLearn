using BannerModule.Services;
using BannerModule.Services.DTOs.Query;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Banners
{
    public class IndexModel : BaseRazor
    {
        private readonly IBannerService _bannerService;

        public IndexModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public List<BannerDto> Banners { get; set; }
        public async Task OnGet()
        {
            Banners = await _bannerService.GetAllBanners();
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => _bannerService.DeleteBanner(id));
        }
    }
}

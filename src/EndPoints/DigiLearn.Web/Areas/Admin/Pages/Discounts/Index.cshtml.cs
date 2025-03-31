using BlogModule.Services.DTOs.Command;
using BlogModule.Services;
using CoreModule.Domain.Discounts.Models;
using CoreModule.Facade.Discounts;
using CoreModule.Query.Dicounts._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Areas.Admin.Pages.Discounts
{
    public class IndexModel : BaseRazor
    {
        private readonly IDiscountFacade _discountFacade;

        public IndexModel(IDiscountFacade discountFacade)
        {
            _discountFacade = discountFacade;
        }

        [BindProperty]
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Code { get; init; }
        [BindProperty]
        [Display(Name = "مقدار ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int Value { get; init; }
        [BindProperty]
        [Display(Name = "نوع ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public DiscountType Type { get; init; }
        [BindProperty]
        [Display(Name = "تاریخ انقضا ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public DateTime ExpirationDate { get; init; }
        [BindProperty]
        [Display(Name = "تعداد حداکثر استفاده ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int MaxUsed { get; init; }
  

        public List<DiscountCodeDto> Discounts { get; set; } =new ();
        public async Task OnGet()
        {
            Discounts= await _discountFacade.GetAll();
        }

        public async Task<IActionResult> OnPostDelete(string code)
        {
            return await AjaxTryCatch(() => _discountFacade.Delete(new CoreModule.Application.Discounts.Delete.DeleteDiscountCommand(code)));
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _discountFacade.Create(new CoreModule.Application.Discounts.Create.CreateDiscountCodeCommand()
            {
              Code=Code,
              Type=Type,
              ExpirationDate=ExpirationDate,
              MaxUsed=MaxUsed,
              Value=Value
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}

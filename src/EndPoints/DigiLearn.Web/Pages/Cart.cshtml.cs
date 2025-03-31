using CoreModule.Application.Discounts.Apply;
using CoreModule.Application.Order.AddItem;
using CoreModule.Application.Order.RemoveItem;
using CoreModule.Domain.Courses.Models;
using CoreModule.Facade.Discounts;
using CoreModule.Facade.Orders;
using CoreModule.Query.Order._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using TransactionModule.Domain;
using TransactionModule.Services.DTOs.Commands;

namespace DigiLearn.Web.Pages;

public class CartModel : BaseRazor
{
    private readonly IOrderFacade _orderFacade;
    private readonly IDiscountFacade _discountFacade;

    public CartModel(IOrderFacade orderFacade, IDiscountFacade discountFacade)
    {
        _orderFacade = orderFacade;
        _discountFacade = discountFacade;
    }

    public OrderDto? Order { get; set; }

    [BindProperty]
    public string DicountCode { get; set; }

    public async Task OnGet()
    {
        Order = await _orderFacade.GetCurrentOrder(User.GetUserId());
    }

    public async Task<IActionResult> OnPostDicountCode()
    {
        var result = await _discountFacade.ApplyDicount(new ApplyDicountDicountCommand()
        {
            Code = DicountCode,
            userId = User.GetUserId()
        });
        return RedirectAndShowAlert(result, RedirectToPage("/cart"));
    }

        public IActionResult OnPostPayment()
    {
        return RedirectToAction("CreateTransaction", "Transaction", new CreateTransactionCommand()
        {
            LinkId = Guid.NewGuid(),
            PaymentAmount = 0,
            UserId = User.GetUserId(),
            PaymentGateway = PaymentGateway.ZarinPal,
            TransactionFor = TransactionFor.CourseOrder
        });
    }
    public IActionResult OnPostPayWithWallet()
    {
        return RedirectToAction("CreateTransaction", "Transaction", new CreateTransactionCommand()
        {
            LinkId = Guid.NewGuid(),
            PaymentAmount = 0,
            UserId = User.GetUserId(),
            PaymentGateway = PaymentGateway.Wallet,
            TransactionFor = TransactionFor.CourseOrder
        });
    }
    public async Task<IActionResult> OnGetAddItem(Guid courseId)
    {
        var result = await _orderFacade.AddItem(new AddOrderItemCommand(User.GetUserId(), courseId));
        return RedirectAndShowAlert(result, RedirectToPage("/cart"));
    }
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _orderFacade.RemoveItem(new RemoveOrderItemCommand(User.GetUserId(), id)));
    }
}

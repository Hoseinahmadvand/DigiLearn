using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using UserModule.Core.Commands.Users.Wallet.IncreaseBalance;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class WalletModel : BaseRazor
{
    private IUserFacade _userFacade;

    public WalletModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public UserDto UserDto { get; set; }
    [BindProperty]
    public int Amount { get; set; }
    public async Task OnGet()
    {
        UserDto = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
    }
    public async Task<IActionResult> OnPost()
    {
       
        var res = await _userFacade.IncreaseWalletBalance(new IncreaseWalletBalanceCommand()
        {
            Amount = Amount,
            UserId = User.GetUserId()
        });

        return RedirectAndShowAlert(res, Redirect("/profile/wallet"));
    }
}

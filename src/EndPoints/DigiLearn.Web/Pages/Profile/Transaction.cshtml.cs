using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using TransactionModule.Services;
using TransactionModule.Services.DTOs.Queries;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class TransactionModel : BaseRazor
{
    private IUserFacade _userFacade;
    private readonly IUserTransactionService _userTransaction;

    public TransactionModel(IUserTransactionService userTransaction, IUserFacade userFacade)
    {
        _userTransaction = userTransaction;
        _userFacade = userFacade;
    }

    public UserDto UserDto { get; set; }
    public List<UserTransactionDto> Transactions { get; set; }
    public async Task OnGet()
    {
       // UserDto = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        Transactions = await _userTransaction.GetTransactionUser(User.GetUserId()); 
    }
}

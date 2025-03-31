using Microsoft.AspNetCore.Mvc.RazorPages;
using TransactionModule.Services;
using TransactionModule.Services.DTOs.Queries;

namespace DigiLearn.Web.Pages.Profile;

public class TransactionModel : PageModel
{
    private readonly IUserTransactionService _userTransaction;

    public TransactionModel(IUserTransactionService userTransaction)
    {
        _userTransaction = userTransaction;
    }
    public List<UserTransactionDto> Transactions { get; set; }
    public void OnGet()
    {
    }
}

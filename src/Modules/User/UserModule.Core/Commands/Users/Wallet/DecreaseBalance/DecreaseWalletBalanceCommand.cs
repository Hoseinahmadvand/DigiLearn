using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Services;

namespace UserModule.Core.Commands.Users.Wallet.DecreaseBalance;

public class DecreaseWalletBalanceCommand : IBaseCommand
{
  
    public int Amount { get; set; }
    public Guid UserId  { get; set; }
}

public class DecreaseWalletBalanceCommandHandeler : IBaseCommandHandler<DecreaseWalletBalanceCommand>
{
    private readonly UserContext _context;
    private readonly INotificationFacade _notification;

    public DecreaseWalletBalanceCommandHandeler(UserContext context, INotificationFacade notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<OperationResult> Handle(DecreaseWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        if (request.Amount > user.Wallet)
        {
            return OperationResult.Error("!موجودی کافی نمیباشد");
        }

        user.Wallet = user.Wallet - request.Amount;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _notification.Create(new CreateNotificationCommand()
        {
            UserId = request.UserId,
            Title = ";برداشت از کیف پول",
            Text = $"از کیف پول شمت مبلغ  {request.Amount} تومان برداشت شد ."

        });
        return OperationResult.Success();
    }
}

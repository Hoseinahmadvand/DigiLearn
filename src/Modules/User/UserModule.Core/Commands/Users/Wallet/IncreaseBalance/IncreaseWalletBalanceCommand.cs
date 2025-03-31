using Common.Application;
using Common.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Services;

namespace UserModule.Core.Commands.Users.Wallet.IncreaseBalance;

public class IncreaseWalletBalanceCommand: IBaseCommand
{
    public Guid UserId { get; set; }
    public int Amount { get; set; }
}

public class IncreaseWalletBalanceCommandHandeler : IBaseCommandHandler<IncreaseWalletBalanceCommand>
{
    private readonly UserContext _context;
    private readonly INotificationFacade _notification;

    public IncreaseWalletBalanceCommandHandeler(UserContext context, INotificationFacade notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<OperationResult> Handle(IncreaseWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        user.Wallet = user.Wallet + request.Amount;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _notification.Create(new CreateNotificationCommand()
        {
            UserId = request.UserId,
            Title = "شارژ کیف پول",
            Text = $"کیف پول شما به مبلغ {request.Amount} تومان شارژ شد ."

        });
        return OperationResult.Success();
    }
}

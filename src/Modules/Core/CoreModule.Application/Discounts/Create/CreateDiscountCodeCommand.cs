using Common.Application;
using CoreModule.Domain.Dicounts.Repository;
using CoreModule.Domain.Discounts.Models;

namespace CoreModule.Application.Discounts.Create;

public class CreateDiscountCodeCommand : IBaseCommand
{
    public string Code { get; set; }
    public int Value { get; set; }
    public int MaxUsed { get; set; }
    public DiscountType Type { get; set; }
    public DateTime ExpirationDate { get; set; }

}

public class CreateDiscountCodeCommandHandler : IBaseCommandHandler<CreateDiscountCodeCommand>
{
    private readonly IDiscountRepository _repository;

    public CreateDiscountCodeCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = new DiscountCode(request.Code, request.Value, request.Type, request.ExpirationDate, request.MaxUsed);

        _repository.Add(discountCode);
        await _repository.Save();
        return OperationResult.Success();
    }
}

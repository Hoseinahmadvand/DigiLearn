using Common.Application;
using CoreModule.Domain.Dicounts.Repository;

namespace CoreModule.Application.Discounts.Delete;

public record DeleteDiscountCommand(string Code) : IBaseCommand;
public class DeleteDiscountCommandHandler : IBaseCommandHandler<DeleteDiscountCommand>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<OperationResult> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByCodeAsync(request.Code);
        if (discount == null)
            throw new Exception("کد تخفیف یافت نشد.");

        discount.IsActive = false; // به جای حذف واقعی، غیرفعال می‌کنیم
        _discountRepository.Update(discount);
        return OperationResult.Success();
    }
}

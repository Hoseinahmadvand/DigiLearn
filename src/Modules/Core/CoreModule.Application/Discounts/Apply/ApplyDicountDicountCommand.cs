using Common.Application;
using CoreModule.Domain.Dicounts.Repository;
using CoreModule.Domain.Discounts.Models;
using CoreModule.Domain.Order.Repositories;

namespace CoreModule.Application.Discounts.Apply;

public class ApplyDicountDicountCommand : IBaseCommand
{
    public Guid userId { get; set; }
    public string Code { get; set; }
}
public class ApplyDicountDicountCommandHandler : IBaseCommandHandler<ApplyDicountDicountCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountRepository _discountRrepository;

    public ApplyDicountDicountCommandHandler(IOrderRepository orderRepository, IDiscountRepository discountRrepository)
    {
        _orderRepository = orderRepository;
        _discountRrepository = discountRrepository;
    }

    public async Task<OperationResult> Handle(ApplyDicountDicountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRrepository.GetByCodeAsync(request.Code);
        var order = await _orderRepository.GetCurrentOrderByUserId(request.userId);

        if (order.DiscountCode != null)
            return OperationResult.Error("بیشتر از یک کد تخفیف نمیشد وارد کرد !");
        if (discount == null)
            return OperationResult.NotFound();
        //if (discount.IsValid() || !discount.IsActive)
        //    return OperationResult.Error("کد تخفیف نا معتبر است !");
        if (discount.CanNotBeUsed())
            return OperationResult.Error(" تعداد حداکثر ظرفیت استفاده از کد تخفیف پر شده !");
        bool used = await _discountRrepository.HasUserUsedDiscountAsync(request.userId, request.Code);
        if (used == true)
            return OperationResult.Error("قبلن از این تخفیف استفاده کرده اید !");

        int discountAmount = discount.Type == DiscountType.Percentage ? (order.TotalPrice * discount.Value) / 100 : discount.Value;

       order.ApplyDicount(discountAmount, request.Code);
        await _orderRepository.Save();

        discount.UseCode();
        await _discountRrepository.AddUsedDiscountAsync(new UsedDiscount(request.userId, request.Code));
        await _discountRrepository.Save();

        return OperationResult.Success();
    }
}
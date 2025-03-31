using CoreModule.Domain.Dicounts.Repository;
using CoreModule.Domain.Discounts;
using CoreModule.Domain.Discounts.Models;

namespace CoreModule.Application;

public class DiscountDomainService : IDiscountDomainService
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountDomainService(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<int> ApplyDiscountAsync(string discountCode, Guid userId, int totalAmount)
    {
        var discount = await _discountRepository.GetByCodeAsync(discountCode);
        if (discount == null)
            throw new Exception("کد تخفیف یافت نشد یا غیرفعال است.");

        if (!discount.IsValid())
            throw new Exception("کد تخفیف منقضی شده است.");

        if (discount.CanNotBeUsed())
            throw new Exception("محدودیت استفاده از کد تخفیف به پایان رسیده است.");

        if (await _discountRepository.HasUserUsedDiscountAsync(userId, discountCode))
            throw new Exception("کاربر قبلاً از این کد تخفیف استفاده کرده است.");

        var discountAmount = discount.CalculateDiscount(totalAmount);
        discount.UseCode();

        var usedDiscount = new UsedDiscount(userId, discountCode);
        await _discountRepository.AddUsedDiscountAsync(usedDiscount);

        return discountAmount;
    }

    public async Task<bool> CanUserUseDiscountAsync(string discountCode, Guid userId)
    {
        var discount = await _discountRepository.GetByCodeAsync(discountCode);
        if (discount == null || !discount.IsValid() || discount.CanNotBeUsed())
            return false;

        return !await _discountRepository.HasUserUsedDiscountAsync(userId, discountCode);
    }
}
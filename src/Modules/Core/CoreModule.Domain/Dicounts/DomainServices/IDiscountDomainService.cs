namespace CoreModule.Domain.Discounts;

public interface IDiscountDomainService
{
    Task<int> ApplyDiscountAsync(string discountCode, Guid userId, int totalAmount);
    Task<bool> CanUserUseDiscountAsync(string discountCode, Guid userId);
}
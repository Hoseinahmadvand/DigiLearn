using Common.Domain.Repository;
using CoreModule.Domain.Discounts.Models;

namespace CoreModule.Domain.Dicounts.Repository;

public interface IDiscountRepository : IBaseRepository<DiscountCode>
{

    Task<DiscountCode> GetByCodeAsync(string code);
    Task<bool> HasUserUsedDiscountAsync(Guid userId, string discountCode);
    Task AddUsedDiscountAsync(UsedDiscount usedDiscount);
}

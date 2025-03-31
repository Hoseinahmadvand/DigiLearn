using Common.Infrastructure.Repository;
using CoreModule.Domain.Dicounts.Repository;
using CoreModule.Domain.Discounts.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.Dicount;

class DiscountRepository : BaseRepository<DiscountCode, CoreModuleEfContext>, IDiscountRepository
{

    public DiscountRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public async Task AddUsedDiscountAsync(UsedDiscount usedDiscount)
    {
        await Context.UsedDiscounts.AddAsync(usedDiscount);
    }

    public async Task<DiscountCode> GetByCodeAsync(string code)
    {
        return await Context.DiscountCodes
          .FirstOrDefaultAsync(d => d.Code == code && d.IsActive);
    }

    public async Task<bool> HasUserUsedDiscountAsync(Guid userId, string discountCode)
    {
        return await Context.UsedDiscounts
           .AnyAsync(ud => ud.UserId == userId && ud.DiscountCode == discountCode);
    }
}

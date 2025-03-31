using Common.Query;
using Common.Query.Filter;
using CoreModule.Domain.Discounts.Models;

namespace CoreModule.Query.Dicounts._DTOs;

public class DiscountCodeDto : BaseDto
{

    public string Code { get; init; }
    public int Value { get; init; }
    public string Type { get; init; }
    public DateTime ExpirationDate { get; init; }
    public DiscountType DiscountType { get; set; }
    public int MaxUsed { get; init; }
    public int CurrentUsed { get; init; }
    public bool IsActive { get; init; }
    public bool IsExpired => DateTime.UtcNow > ExpirationDate;
}



public class UsedDiscountDto : BaseDto
{

    public Guid UserId { get; init; }
    public string DiscountCode { get; init; }

}

public record DiscountUsageSummaryDto
{
    public string Code { get; init; }
    public int UsageCount { get; init; }
    public int MaxUsed { get; init; }
    public bool HasReachedLimit => UsageCount >= MaxUsed;
}


public class DiscountFilterDto : BaseFilterParam
{
    public string Type { get; init; } // "Percentage" یا "FixedAmount"
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool? IsActive { get; init; }
}
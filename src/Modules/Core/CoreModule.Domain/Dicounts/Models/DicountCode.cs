using Common.Domain;

namespace CoreModule.Domain.Discounts.Models;

public enum DiscountType
{
    Percentage,
    FixedAmount
}

public class DiscountCode : BaseEntity
{
    public string Code { get; private set; }
    public int Value { get; private set; }
    public int MaxUsed { get; private set; }
    public int CurrentUsed { get; private set; }
    public DiscountType Type { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; set; }
    private DiscountCode() { }

    public DiscountCode(string code, int value, DiscountType type, DateTime expirationDate, int maxUsed)
    {
        Code = code;
        Value = value;
        Type = type;
        ExpirationDate = expirationDate;
        MaxUsed = maxUsed;
        CurrentUsed = 0;
        IsActive = true;
    }

    public bool IsValid()
    {
        return DateTime.UtcNow <= ExpirationDate;
    }

    public bool CanNotBeUsed()
    {
        return CurrentUsed >= MaxUsed;
    }

    public int CalculateDiscount(int totalAmount)
    {
        return Type == DiscountType.Percentage
            ? (int)(totalAmount * (Value / 100.0))
            : Value;
    }

    public void UseCode()
    {
            CurrentUsed++;
    }
}

public class UsedDiscount : BaseEntity
{
    public Guid UserId { get; private set; }
    public string DiscountCode { get; private set; }

    private UsedDiscount() { }

    public UsedDiscount(Guid userId, string discountCode)
    {

        UserId = userId;
        DiscountCode = discountCode;
    }
}
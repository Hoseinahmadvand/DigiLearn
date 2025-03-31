using Common.Domain;
using CoreModule.Domain.Discounts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("DiscountCodes", Schema = "dbo")]
public class DiscountCodeModel :BaseEntity
{
    public string Code { get; private set; }
    public int Value { get; private set; }
    public int MaxUsed { get; private set; }
    public int CurrentUsed { get; private set; }
    public DiscountType Type { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; set; }
}


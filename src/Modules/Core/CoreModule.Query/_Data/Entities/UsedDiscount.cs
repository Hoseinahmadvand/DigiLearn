using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("UsedDiscounts", Schema = "dbo")]
public class UsedDiscount : BaseEntity
{
    public Guid UserId { get; private set; }
    public string DiscountCode { get; private set; }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace UserModule.Data.Entities.Users;


[Index("Email", IsUnique = true)]
[Index("PhoneNumber", IsUnique = true)]
[Table("User", Schema = "user")]
public class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [MaxLength(11)]
    [Required]
    public string PhoneNumber { get; set; }


    [MaxLength(50)]
    public string? Email { get; set; }

    public int Wallet { get; set; } = 0;

    [MaxLength(70)]
    [Required]
    public string Password { get; set; }

    [MaxLength(100)]
    [Required]
    public string Avatar { get; set; }

    public List<UserRole> UserRoles { get; set; }
}

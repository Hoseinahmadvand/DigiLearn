using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserModule.Data.Entities.Users;

[Table("UserRole", Schema = "user")]
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }


    public User User { get; set; }
    public Role Role { get; set; }
}
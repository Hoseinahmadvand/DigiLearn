using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using UserModule.Data.Entities._Enums;

[Table("RolePermission", Schema = "user")]
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Permissions Permissions { get; set; }
}
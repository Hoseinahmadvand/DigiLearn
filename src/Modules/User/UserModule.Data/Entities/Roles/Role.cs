using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Role", Schema = "user")]
public class Role : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }


    public List<RolePermission> Permissions { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace UserModule.Data.Entities.Notifications;

[Table("UserNotification", Schema = "user")]
public class UserNotification : BaseEntity
{
    public Guid UserId { get; set; }

    [MaxLength(2000)]
    public string Text { get; set; }
    [MaxLength(300)]
    public string Title { get; set; }
    public bool IsSeen { get; set; }
}
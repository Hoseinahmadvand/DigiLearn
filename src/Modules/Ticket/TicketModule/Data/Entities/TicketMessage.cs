using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace TicketModule.Data.Entities;

[Table("TicketMessage", Schema = "ticket")]
class TicketMessage : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }

    [MaxLength(100)]
    public string UserFullName { get; set; }
    public string Text { get; set; }


    public Ticket Ticket { get; set; }
}
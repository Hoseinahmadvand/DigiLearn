using Common.Application;
using TicketModule.Core.DTOs.Tickets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketModule.Core.Services;

public interface ITicketService
{
    Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command);
    Task<OperationResult> CloseTicket(Guid ticketId);

    Task<TicketDto?> GetTicket(Guid ticketId);
    Task<TicketFilterResult> GetTicketsByFilter(TicketFilterParams filterParams);
}

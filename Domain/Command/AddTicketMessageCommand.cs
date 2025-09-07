using Domain.DTOs;
using MediatR;

namespace Domain.Command;

public class AddTicketMessageCommand: IRequest<TicketMessageDto>
{
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public string Body { get; set; }
}
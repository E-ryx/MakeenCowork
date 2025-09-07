using Domain.DTOs;
using MediatR;

namespace Domain.Queries;

public class GetTicketMessagesQuery: IRequest<IReadOnlyList<TicketMessageDto>>
{
    public int TicketId { get; set; }
    public int UserId { get; set; }
}
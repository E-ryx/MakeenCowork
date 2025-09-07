using Domain.DTOs;
using Domain.Interfaces;
using Domain.Queries;
using MediatR;

namespace Domain.CommandHandlers;

public class GetTicketMessagesHandler(ITicketChatService _ticketChatService): IRequestHandler<GetTicketMessagesQuery, IReadOnlyList<TicketMessageDto>>
{
    public async Task<IReadOnlyList<TicketMessageDto>> Handle(
        GetTicketMessagesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _ticketChatService.GetMessagesAsync(request.TicketId, request.UserId);
    }
}
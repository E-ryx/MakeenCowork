using Domain.Command;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers;

public class AddTicketMessageHandler(ITicketChatService _ticketChatService): IRequestHandler<AddTicketMessageCommand, TicketMessageDto>
{
    public async Task<TicketMessageDto> Handle(
        AddTicketMessageCommand request, 
        CancellationToken cancellationToken)
    {
        return await _ticketChatService.AddMessageAsync(
            request.TicketId, request.UserId, request.Body);
    }
}
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.CommandHandlers;

public class CreateTicketHandler(ITicketRepository _ticketRepository): IRequestHandler<CreateTicketCommand>
{
    public async Task Handle(CreateTicketCommand command, CancellationToken ct)
    {
        await _ticketRepository.AddTicketAsync(new Ticket(command.UserId));
    }
}
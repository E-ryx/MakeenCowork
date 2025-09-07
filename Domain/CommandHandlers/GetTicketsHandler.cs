using Domain.DTOs;
using Domain.Interfaces;
using Domain.Queries;
using MediatR;

namespace Domain.CommandHandlers;

public class GetTicketsHandler(ITicketRepository _ticketRepository): IRequestHandler<GetUserTicketsQuery, IEnumerable<TicketsDto>>
{
    public async Task<IEnumerable<TicketsDto>> Handle(GetUserTicketsQuery request, CancellationToken ct)
    {
        return await _ticketRepository.GetUserTicketsAsync(request.UserId);
    }
}
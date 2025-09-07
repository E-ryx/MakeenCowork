using Domain.DTOs;
using MediatR;

namespace Domain.Queries;

public class GetUserTicketsQuery: IRequest<IEnumerable<TicketsDto>>
{
    public int UserId { get; set; }
}
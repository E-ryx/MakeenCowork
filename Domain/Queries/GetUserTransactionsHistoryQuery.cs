using Domain.DTOs;
using MediatR;

namespace Domain.Queries;

public class GetUserTransactionsHistoryQuery: IRequest<IEnumerable<UserTransactionsHistoryDto>>
{
    public int UserId { get; set; }
}
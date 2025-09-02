using Domain.DTOs;
using Domain.Interfaces;
using Domain.Queries;
using MediatR;

namespace Domain.CommandHandlers.User;

public class GetUserTransactionsHistoryHandler: IRequestHandler<GetUserTransactionsHistoryQuery, IEnumerable<UserTransactionsHistoryDto>>
{
    private readonly ITransactionRepositroy _transactionRepositroy;

    public GetUserTransactionsHistoryHandler(ITransactionRepositroy transactionRepositroy)
    {
        _transactionRepositroy = transactionRepositroy;
    }

    public async Task<IEnumerable<UserTransactionsHistoryDto>> Handle(GetUserTransactionsHistoryQuery query, CancellationToken ct)
    {
        return await _transactionRepositroy.GetAllTransactionsAsync(query.UserId);
    }
}
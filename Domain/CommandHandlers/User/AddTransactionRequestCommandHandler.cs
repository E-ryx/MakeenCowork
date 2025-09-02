using Domain.Command;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers.User;

public class AddTransactionRequestCommandHandler: IRequestHandler<AddTransactionRequestCommand, string>
{
    private readonly ITransactionRepositroy _transactionRepositroy;

    public AddTransactionRequestCommandHandler(ITransactionRepositroy transactionRepositroy)
    {
        _transactionRepositroy = transactionRepositroy;
    }

    public async Task<string> Handle(AddTransactionRequestCommand command, CancellationToken ct)
    {
        return await _transactionRepositroy.AddTransactionRequestAsync(command.UserId, command.Price,
            EnumCollection.TransactionType.Increase);
    }
}
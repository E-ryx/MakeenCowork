using Domain.DTOs;
using Domain.Enums;

namespace Domain.Interfaces;

public interface ITransactionRepositroy
{
    Task<string> DeclineTransactionAsync(int userId, string trackingCode);
    Task<string> ApproveTransactionAsync(int userId, string trackingCode);
    Task<string> AddTransactionRequestAsync(int userId, int amount, EnumCollection.TransactionType transactionType);
    Task<string> AddReservationTransactionAsync(int userId, int amount, EnumCollection.TransactionType transactionType);
    Task<UserTransactionDto> GetTransactionAsync(int userId, string trackingCode);
    Task<IEnumerable<UserTransactionDto>> GetAllTransactionsAsync();
    Task<IEnumerable<UserTransactionsHistoryDto>> GetAllTransactionsAsync(int userId);
}
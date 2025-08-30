using Domain.DTOs;

namespace Domain.Interfaces;

public interface ITransactionRepositroy
{
    Task<string> AddTransactionAsync(int userId, int amount);
    Task<UserTransactionDto> GetTransactionAsync(int userId, string trackingCode);
    Task<IEnumerable<UserTransactionDto>> GetAllTransactionsAsync();
    Task<IEnumerable<UserTransactionDto>> GetAllTransactionsAsync(int userId);
}
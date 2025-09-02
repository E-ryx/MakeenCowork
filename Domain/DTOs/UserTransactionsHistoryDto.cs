using Domain.Enums;

namespace Domain.DTOs;

public class UserTransactionsHistoryDto
{
    public DateTime CreatedAt { get; set; }
    public int Price { get; set; }
    public string TrackingCode { get; set; }
    public EnumCollection.TransactionStatus TransactionStatus { get; set; }
    public EnumCollection.TransactionType TransactionType { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Transaction
    {
        public Transaction(int amount, DateTime createdAt, int userId)
        {
            Amount = amount;
            TrackingCode = GenerateTrackingCode();
            CreatedAt = DateTime.Now;
            UserId = userId;
        }
        public int TransactionId { get; private set; }
        public int Amount { get; private set; }
        public string TrackingCode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int? ImageId { get; private set; }
        private string GenerateTrackingCode()
        {
            return $"TRX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
        public Transaction() { }
    }
}

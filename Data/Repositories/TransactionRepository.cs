using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TransactionRepository
    {
        private readonly MyDbContext _context;
        public TransactionRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddTransactionAsync(int userId, int amount)
        {
            var transaction = new Domain.Models.Transaction(amount, DateTime.Now, userId);
            await _context.SaveChangesAsync();
            return transaction.TrackingCode;
        }

        public async Task<UserTransactionDto> GetTransactionAsync(int userId, string trackingCode)
        {
            var transaction = await _context.Transactions.Include(x => x.User)
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TrackingCode == trackingCode);
            var transactionDto = new UserTransactionDto
            {
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                TrackingCode = transaction.TrackingCode,
                UserFullName = $"{transaction.User.Name} {transaction.User.FamilyName}"
            };

            return transactionDto;
        }

        public async Task<IEnumerable<UserTransactionDto>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Include(x => x.User)
                .Select(transaction => new UserTransactionDto
                {
                    Amount = transaction.Amount,
                    CreatedAt = transaction.CreatedAt,
                    TrackingCode = transaction.TrackingCode,
                    UserFullName = $"{transaction.User.Name} {transaction.User.FamilyName}"
                }).ToListAsync();
        }
        public async Task<IEnumerable<UserTransactionDto>> GetAllTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .Select(transaction => new UserTransactionDto
                {
                    Amount = transaction.Amount,
                    CreatedAt = transaction.CreatedAt,
                    TrackingCode = transaction.TrackingCode,
                    UserFullName = $"{transaction.User.Name} {transaction.User.FamilyName}"
                })
                .ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TransactionRepository: ITransactionRepositroy
    {
        private readonly MyDbContext _context;
        public TransactionRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<string> ApproveTransactionAsync(int userId, string trackingCode)
        {
            var transaction =
                await _context.Transactions.FirstOrDefaultAsync(x => x.User.Id == userId && x.TrackingCode == trackingCode);
            if (transaction == null)
                return $"Transaction {trackingCode} Not Found";
            transaction.UpdateTransactionStatus(EnumCollection.TransactionStatus.Approved);
            await _context.SaveChangesAsync();
            return $"Transaction {trackingCode} Approved";
        }

        public async Task<string> DeclineTransactionAsync(int userId, string trackingCode)
        {
            var transaction =
                await _context.Transactions.FirstOrDefaultAsync(x => x.User.Id == userId && x.TrackingCode == trackingCode);
            if (transaction == null)
                return $"Transaction {trackingCode} Not Found";
            transaction.UpdateTransactionStatus(EnumCollection.TransactionStatus.Declined);
            await _context.SaveChangesAsync();
            return $"Transaction {trackingCode} Declined";
        }
        public async Task<string> AddTransactionRequestAsync(int userId, int amount, EnumCollection.TransactionType transactionType)
        {
            var transaction = new Domain.Models.Transaction(amount, userId, transactionType);
            await _context.SaveChangesAsync();
            return transaction.TrackingCode;
        }

        public async Task<string> AddReservationTransactionAsync(int userId, int amount, EnumCollection.TransactionType transactionType)
        {
            var transaction = new Domain.Models.Transaction(amount, userId, transactionType);
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
        public async Task<IEnumerable<UserTransactionsHistoryDto>> GetAllTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .Select(transaction => new UserTransactionsHistoryDto()
                {
                    Price = transaction.Amount,
                    CreatedAt = transaction.CreatedAt,
                    TrackingCode = transaction.TrackingCode,
                    TransactionStatus = transaction.TransactionStatus,
                    TransactionType = transaction.TransactionType
                })
                .ToListAsync();
        }
    }
}

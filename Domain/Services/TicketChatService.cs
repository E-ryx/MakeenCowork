using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

    public class TicketChatService(ITicketRepository _ticketRepository) : ITicketChatService
    {
        public async Task<bool> UserCanAccessTicketAsync(int ticketId, int userId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket is null) return false;

            var isCreator = ticket.UserId == userId;
            var isAssignedAgent = ticket.AdminId == userId;

            return isCreator || isAssignedAgent;
        }

        public async Task<TicketMessageDto> AddMessageAsync(int ticketId, int userId, string body)
        {
            var entity = new TicketMessage(ticketId, userId, body);

            await _ticketRepository.AddTicketMessageAsync(entity);

            return new TicketMessageDto
            {
                TicketMessageId = entity.TicketMessageId,
                TicketId = entity.TicketId,
                SenderUserId = entity.SenderUserId,
                Body = entity.Body,
                SentAt = entity.SentAt
            };
        }
        public async Task<IReadOnlyList<TicketMessageDto>> GetMessagesAsync(int ticketId, int userId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket is null)
                throw new KeyNotFoundException($"Ticket {ticketId} not found.");

            var isCreator = ticket.UserId == userId;
            var isAssignedAgent = ticket.AdminId == userId;

            if (!(isCreator || isAssignedAgent))
                throw new UnauthorizedAccessException("You are not allowed to view this ticket.");

            // Load messages
            var messages = await _ticketRepository
                .GetByTicketIdAsync(ticketId);

            // Project to DTOs
            return messages
                .OrderBy(m => m.SentAt)
                .Select(m => new TicketMessageDto
                {
                    TicketMessageId = m.TicketMessageId,
                    TicketId = m.TicketId,
                    SenderUserId = m.SenderUserId,
                    Body = m.Body,
                    SentAt = m.SentAt
                })
                .ToList();
        }
    }
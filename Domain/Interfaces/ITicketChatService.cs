using Domain.DTOs;

namespace Domain.Interfaces;

public interface ITicketChatService
{
        Task<bool> UserCanAccessTicketAsync(int ticketId, int userId);
        Task<TicketMessageDto> AddMessageAsync(int ticketId, int userId, string body);
        Task<IReadOnlyList<TicketMessageDto>> GetMessagesAsync(int ticketId, int userId);
}
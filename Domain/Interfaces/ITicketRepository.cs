using Domain.DTOs;
using Domain.Models;

namespace Domain.Interfaces;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(int ticketId);
    Task AddTicketMessageAsync(TicketMessage ticketMessage);
    Task AddTicketAsync(Ticket ticket);
    Task<List<TicketMessage>?> GetByTicketIdAsync(int ticketId);
    Task<IEnumerable<TicketsDto>> GetUserTicketsAsync(int userId);
}
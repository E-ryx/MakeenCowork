using Data.Context;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TicketRepository(MyDbContext _context) : ITicketRepository
{
    public Task<Ticket?> GetByIdAsync(int ticketId) =>
        _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.TicketId == ticketId);

    public async Task<List<TicketMessage>?> GetByTicketIdAsync(int ticketId) =>
        await _context.TicketMessages.AsNoTracking().Where(t => t.TicketId == ticketId).ToListAsync();
    public async Task AddTicketMessageAsync(TicketMessage ticketMessage)
    {
        await _context.TicketMessages.AddAsync(ticketMessage);
        await _context.SaveChangesAsync();
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TicketsDto>> GetUserTicketsAsync(int userId) => await _context.Tickets
        .Where(x => x.UserId == userId).Select(x => new TicketsDto()
        {
            TicketId = x.TicketId,
            CreatedAt = x.CreatedAt,
            TicketNumber = x.TicketNumber
        }).ToListAsync();

}
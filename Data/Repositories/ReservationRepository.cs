using Data.Context;
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly MyDbContext _context;

    public ReservationRepository(MyDbContext context)
    {
        _context = context;
    }




    public async Task<int> AddReserve(AddReservationCommand command)
    {
        var Reserv = new Reservation(command.UserId, command.SpaceId, command.TransactionId, command.NumberOfPeople, Reservation.ReservationStatus.Pending, command.ExtraServices, DateOnly.FromDateTime(DateTime.Now));
        await _context.AddAsync(Reserv);
        await _context.SaveChangesAsync();
        return Reserv.ReservationId;
    }

    public async Task AddReservatioDayAsync(ReservationDay day)
    {
        await _context.ReservationDays.AddAsync(day);
        await _context.SaveChangesAsync();
    }

    public async Task CancelReservationAsync(Reservation day)
    {
        var Day = await _context.ReservationDays.FirstOrDefaultAsync(a => a.ReservationId == day.ReservationId && a.UserId == a.UserId);
        _context.Remove(Day);
        _context.Remove(day);
        await _context.SaveChangesAsync();
    }

    public async Task<Reservation> GetReserveByIdAsync(int Id)
    {
        return await _context.Reservations.FirstOrDefaultAsync(a => a.ReservationId == Id);
    }

    public async Task<bool> AnyReservation(int id)
    {
        return await _context.Reservations.AnyAsync(a => a.ReservationId == id);
    }
}
using Data.Context;
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ReservationRepository:IReservationRepository
{
    private readonly MyDbContext _context;

    public ReservationRepository(MyDbContext context)
    {
        _context = context;
    }


    public async Task<double> GetUserBalance(int userid)
    {
        return await _context.Users.Where(a => a.Id == userid).Select(a => a.WalletBalance).FirstOrDefaultAsync();
    }

    public async Task<int> AddReserve(AddReservationCommand command)
    {
        var Reserv = new Reservation(command.UserId,command.SpaceId,command.TransactionId,command.NumberOfPeople,Reservation.ReservationStatus.Pending,command.ExtraServices, DateOnly.FromDateTime(DateTime.Now));
        await _context.AddAsync(Reserv);
        await _context.SaveChangesAsync();
        return Reserv.ReservationId;
    }

    public async Task AddReservatioDayAsync(ReservationDay day)
    {
        await _context.ReservationDays.AddAsync(day);
        await _context.SaveChangesAsync();
    }
}
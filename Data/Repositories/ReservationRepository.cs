using System.Formats.Asn1;
using Data.Context;
using Domain.Command;
using Domain.DTOs;
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

    public async Task<IQueryable<Reservation>> GetAllReservation(GetAllReservationCommand command)
    {
        int CountToSkip = command.Page * 10;
        var AllReservation = _context.Reservations.OrderBy(a => a.CreatedAt).Include(a=>a.User).AsQueryable();
        if (command.DateOnly.HasValue)
        {
            AllReservation = AllReservation.Where(a => a.CreatedAt == command.DateOnly);
        }

        if (command.FullName!=null)
        {
            AllReservation= AllReservation.Where(a => EF.Functions.Like(a.User.Name, $"%{command.FullName}%")&& EF.Functions.Like(a.User.FamilyName, $"%{command.FullName}%"));
        }
        var list= AllReservation.Skip(CountToSkip).Take(10);
        if (list.Count()==0)
        {
          return AllReservation.Take(10);
        }
        else
        {
          return  AllReservation.Skip(CountToSkip).Take(10);
        }






    }
}
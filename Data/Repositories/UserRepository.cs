using Data.Context;
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository: IUserRepository
{
    #region Context
    private readonly MyDbContext _context;
    public UserRepository(MyDbContext context)
    {
        _context = context;
    }
    #endregion

    public async Task CreateUserAsync(RegisterCommand command)
    {
        var user = new User
        {
            BirthDate = command.BirthDate,
            Email = command.Email,
            FamilyName = command.FamilyName,
            ImgOfNationalCardUrl = null,
            Name = command.Name,
            NationalCode = command.NationalCode,
            PhoneNumber = command.PhoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    public async Task<User> GetUserAsync(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber );
    }
    public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
    {
        return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }
    public async Task<User> GetUserAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<Reservation>> GetUserCurrentReservations(int userId, Reservation.ReservationState reservationsState)
    {
        return await _context.Reservations
            .AsNoTracking()
            .Include(r => r.Days)
            .Include(r => r.Space)
            .Where(r => r.UserId == userId && r.State == reservationsState)
            .ToListAsync();    
    }

    public async Task UpdateWalletAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    
}
using Domain.Command;
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(RegisterCommand command);
    Task<User> GetUserAsync(string phoneNumber);
    Task<bool> PhoneNumberExistsAsync(string phoneNumber);
    Task<User> GetUserAsync(int id);
    Task<List<Reservation>> GetUserCurrentReservations(int userId, Reservation.ReservationState reservationsState);
    Task UpdateWalletAsync(User user);
}
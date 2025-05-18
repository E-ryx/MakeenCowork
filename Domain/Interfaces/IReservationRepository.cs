using Domain.Command;
using Domain.Models;

namespace Domain.Interfaces;

public interface IReservationRepository
{
    Task<double> GetUserBalance(int userid);
    Task<int> AddReserve(AddReservationCommand command);
    Task AddReservatioDayAsync(ReservationDay day);
}
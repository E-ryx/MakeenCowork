using Domain.Command;
using Domain.Models;

namespace Domain.Interfaces;

public interface IReservationRepository
{
    Task<int> AddReserve(AddReservationCommand command);
    Task AddReservatioDayAsync(ReservationDay day);
    Task CancelReservationAsync(Reservation day);
    Task<Reservation> GetReserveByIdAsync(int Id);
    Task<bool> AnyReservation(int id);
}
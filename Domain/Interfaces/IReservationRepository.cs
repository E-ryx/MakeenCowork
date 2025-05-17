using Domain.Command;

namespace Domain.Interfaces;

public interface IReservationRepository
{
    Task<double> GetUserBalance(int userid);
    Task AddReserve(AddReservationCommand command);
}
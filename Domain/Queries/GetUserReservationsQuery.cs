using Domain.DTOs;
using Domain.Models;
using MediatR;

namespace Domain.Queries;

public class GetUserReservationsQuery: IRequest<List<UserCurrentReservationDto>>
{
    public int UserId { get; set; }
    public Reservation.ReservationState State { get; set; }
}
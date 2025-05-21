using Domain.DTOs;
using MediatR;

namespace Domain.Command;

public class GetAllReservationCommand:IRequest<List<AllReservationDTO>>
{
    public int Page { get; set; } = 0;
    public DateOnly? DateOnly { get; set; } = null;
    public string? FullName { get; set; }
}
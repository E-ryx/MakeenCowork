using Domain.Command;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers;

public class GetAllReservationCommandHandler:IRequestHandler<GetAllReservationCommand,List<AllReservationDTO>>
{
    private readonly IReservationRepository _reservation;
    public GetAllReservationCommandHandler(IReservationRepository reservation)
    {
        _reservation = reservation;
    }


    public async Task<List<AllReservationDTO>> Handle(GetAllReservationCommand request, CancellationToken cancellationToken)
    {

        var AllReservation = await _reservation.GetAllReservation(request);
        var ListDTO = new List<AllReservationDTO>();
        foreach (var item in AllReservation)
        {
            var DTO = new AllReservationDTO()
            {
                CreateDate = item.CreatedAt,
                FullName = item.User.Name+" "+item.User.FamilyName,
                NationalCode = item.User.NationalCode,
                PhoneNumber = item.User.PhoneNumber,
                ReservationId = item.ReservationId,
                UserId = item.UserId,
            };
            ListDTO.Add(DTO);
        }
        return ListDTO;
    }
}
using Domain.Command;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers;

public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, string>
{
    private readonly IReservationRepository _reservation;
    private readonly ISpaceRepository _spaceRepository;

    public AddReservationCommandHandler(IReservationRepository reservation, ISpaceRepository spaceRepository)
    {
        _reservation = reservation;
        _spaceRepository = spaceRepository;
    }


    public async Task<string> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        var UserBalance = await _reservation.GetUserBalance(request.UserId);
        var SpacePrice = await _spaceRepository.GetPriceOfSpace(request.SpaceId);
        var PriceToPay = request.NumberOfPeople * SpacePrice;
        var CountOfFree = await _spaceRepository.SpaceIsFreeAtDate(request.SpaceId, request.CreatedAt);

        if (UserBalance <= PriceToPay)
            return "Balance Is not Enough";

        if (CountOfFree<= request.NumberOfPeople)
            return "Not Free Chair";

        await _reservation.AddReserve(request);
        return "Reserved";
    }
}
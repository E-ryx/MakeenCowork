using Domain.Command;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers.Reservation;

public class CancelReserveCommandHandler : IRequestHandler<CancelReserveCommand>
{
    private readonly IReservationRepository _reservation;
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public CancelReserveCommandHandler(IReservationRepository reservation, ISpaceRepository spaceRepository, IUserRepository userRepository, IUserService userService)
    {
        _reservation = reservation;
        _spaceRepository = spaceRepository;
        _userRepository = userRepository;
        _userService = userService;
    }


    public async Task Handle(CancelReserveCommand request, CancellationToken cancellationToken)
    {
        var Reserveation = await _reservation.GetReserveByIdAsync(request.ReservationId);
        if (await _reservation.AnyReservation(request.ReservationId))
        {
            var PriceHaveToReturn = await _spaceRepository.GetPriceOfSpace(Reserveation.SpaceId) *
                                    Reserveation.NumberOfPeople;
            
            await _reservation.CancelReservationAsync(Reserveation);
            await _userService.ChangeWalletBalance(EnumCollection.WalletFunction.Increase, PriceHaveToReturn,
                request.UserId);
        }
    }
}
using Domain.Command;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.CommandHandlers;

public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, string>
{
    private readonly IReservationRepository _reservation;
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepositroy _transactionRepositroy;

    public AddReservationCommandHandler(IReservationRepository reservation,
        ISpaceRepository spaceRepository,
        IUserService userService,
        IUserRepository userRepository,
        ITransactionRepositroy transactionRepositroy)
    {
        _reservation = reservation;
        _spaceRepository = spaceRepository;
        _userService = userService;
        _userRepository = userRepository;
        _transactionRepositroy = transactionRepositroy;
    }

    public async Task<string> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        var UserBalance = await _userRepository.GetUserBalance(request.UserId);
        var SpacePrice = await _spaceRepository.GetPriceOfSpace(request.SpaceId);
        var PriceToPay = request.NumberOfPeople * SpacePrice;
        var CountOfFree = await _spaceRepository.SpaceIsFreeAtDate(request.SpaceId, request.CreatedAt);

        if (UserBalance <= PriceToPay)
            return "Balance Is not Enough";

        if (CountOfFree<= request.NumberOfPeople)
            return "Not Free Chair";

        var Id= await _reservation.AddReserve(request);
        await _userService.ChangeWalletBalance(EnumCollection.WalletFunction.Decrease, PriceToPay, request.UserId);
        var transactionTrackingCode = await _transactionRepositroy.AddTransactionAsync(request.UserId, PriceToPay);
        var ReservationDay = new ReservationDay(Id,DateOnly.FromDateTime(DateTime.Now));
        ReservationDay.SpaceId = request.SpaceId;
        await _reservation.AddReservatioDayAsync(ReservationDay);
        return $"Reserved - Transaction Tracking Code: {transactionTrackingCode}";
    }
}
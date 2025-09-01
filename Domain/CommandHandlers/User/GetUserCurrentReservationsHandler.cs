using Domain.DTOs;
using Domain.Interfaces;
using Domain.Queries;
using MediatR;

namespace Domain.CommandHandlers.User;

public class GetUserCurrentReservationsHandler: IRequestHandler<GetUserReservationsQuery, List<UserCurrentReservationDto>>
{
    private readonly IUserService _userService;

    public GetUserCurrentReservationsHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<UserCurrentReservationDto>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserCurrentReservationsAsync(request.UserId, request.State);
    }
}
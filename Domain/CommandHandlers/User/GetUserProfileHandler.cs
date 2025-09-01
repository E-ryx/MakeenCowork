using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Queries;
using MediatR;

namespace Domain.CommandHandlers.User
{
    public class GetUserProfileHandler: IRequestHandler<GetProfileQuery, UserProfileDto>
    {
        private readonly IUserService _userService;

        public GetUserProfileHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserProfile(request.UserId);
        }
    }
}

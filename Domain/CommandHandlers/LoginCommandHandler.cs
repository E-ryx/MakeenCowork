using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Command;
using Domain.Interfaces;
using MediatR;
using static Domain.Enums.EnumCollection;

namespace Domain.CommandHandlers
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUserService _userService;
        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LoginAsync(request);
        }
    }
}

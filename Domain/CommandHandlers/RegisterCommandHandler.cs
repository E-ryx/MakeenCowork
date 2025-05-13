using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Command;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers
{
    public class RegisterCommandHandler: IRequestHandler<RegisterCommand, bool>
    {
        private readonly IUserService _userService;

        public RegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(request);
        }
    }
}

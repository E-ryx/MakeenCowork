using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Command;
using Domain.Interfaces;

namespace Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task RegisterAsync(RegisterCommand command)
        {
            await _userRepository.Register(command);
        }
    }
}

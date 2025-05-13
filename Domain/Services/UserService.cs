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
        private readonly IOtpService _otpService;

        public UserService(IUserRepository userRepository, IOtpService otpService)
        {
            _userRepository = userRepository;
            _otpService = otpService;
        }
        public async Task<bool> RegisterAsync(RegisterCommand command)
        {
            if (await _userRepository.PhoneNumberExistsAsync(command.PhoneNumber))
                throw new Exception("Phone Number Already Taken!");
            if (!_otpService.ValidateOtp(command.PhoneNumber, command.OtpResponse))
                throw new Exception("Wrong Otp Response");
            await _userRepository.CreateUserAsync(command);
            return true;
        }
    }
}

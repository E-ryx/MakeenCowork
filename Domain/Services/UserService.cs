using System;
using System.Collections.Generic;
using System.Linq;
    using System.Security.Claims;
using System.Text;
    using System.Threading.Tasks;
    using Domain.Command;
using Domain.DTOs;
using Domain.Interfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
using static Domain.Enums.EnumCollection;

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

        public async Task<LoginResult> LoginAsync(LoginCommand command)
        {
            if (!await _userRepository.PhoneNumberExistsAsync(command.PhoneNumber))
                return LoginResult.NotFound;
            if (!_otpService.ValidateOtp(command.PhoneNumber, command.OtpResponse))
                return LoginResult.WrongOtp;
            var user = await _userRepository.GetUserAsync(command.PhoneNumber);

            return LoginResult.Succeeded;
        }
        public async Task<UserProfileDto> GetUserProfile(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            if (user == null) throw new Exception("User not found");

            return new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                FamilyName = user.FamilyName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            };
        }
    }
}

using Domain.Command;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(RegisterCommand command);
    Task<bool> PhoneNumberExistsAsync(string phoneNumber);
}
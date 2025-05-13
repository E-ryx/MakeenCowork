using Domain.Command;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task Register(RegisterCommand command);
}
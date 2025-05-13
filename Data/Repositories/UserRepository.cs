using Data.Context;
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class UserRepository: IUserRepository
{
    #region Context
    private readonly MyDbContext _context;
    public UserRepository(MyDbContext context)
    {
        _context = context;
    }
    #endregion

    public async Task Register(RegisterCommand command)
    {
        var user = new User
        {
            BirthDate = command.BirthDate,
            Email = command.Email,
            FamilyName = command.FamilyName,
            ImgOfNationalCardUrl = null,
            Name = command.Name,
            NationalCode = command.NationalCode,
            PhoneNumber = command.PhoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

}
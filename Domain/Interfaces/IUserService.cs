using Domain.Command;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Queries;
using static Domain.Enums.EnumCollection;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterCommand command);
        Task<LoginResult> LoginAsync(LoginCommand command);
        Task<UserProfileDto> GetUserProfile(int id);
        Task ChangeWalletBalance(WalletFunction function,double Amount,int UserId);
        Task<List<UserCurrentReservationDto>> GetUserCurrentReservationsAsync(int userId, Reservation.ReservationState reservationsState);
    }
}

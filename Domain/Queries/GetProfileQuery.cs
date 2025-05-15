using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using MediatR;

namespace Domain.Queries
{
    public class GetProfileQuery: IRequest<UserProfileDto>
    {
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using static Domain.Enums.EnumCollection;

namespace Domain.Command
{
    public class LoginCommand: IRequest<LoginResult>
    {
        public string PhoneNumber { get; set; }
        public string OtpResponse { get; set; }
    }
}

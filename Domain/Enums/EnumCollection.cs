using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public class EnumCollection
    {
        public enum LoginResult
        {
            NotFound,
            Succeeded,
            WrongOtp
        }
        public enum WalletFunction
        {
            Increase,
            Decrease
        }
    }
}

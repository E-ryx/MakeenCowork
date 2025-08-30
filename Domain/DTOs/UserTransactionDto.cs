using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserTransactionDto
    {
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TrackingCode { get; set; }
        public string UserFullName { get; set; }
    }
}

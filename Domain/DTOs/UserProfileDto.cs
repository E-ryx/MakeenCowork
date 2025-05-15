using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.User;

namespace Domain.DTOs
{
    public class UserProfileDto
    {
    public int Id{ get; set; }
    public string? Email { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }
    public string NationalCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ImgOfNationalCardUrl { get; set; }
    public GenderType? Gender { get; set; }
    public string PhoneNumber { get; set; }
    }
}

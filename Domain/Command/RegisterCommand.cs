using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Command;

public class RegisterCommand: IRequest<bool>
{
    public string? Email{ get; set; }
    public string Name{ get; set; }
    public string FamilyName { get; set; }
    public string NationalCode { get; set; }
    public DateTime? BirthDate { get; set; }
    public IFormFile? ImgOfNationalCard { get; set; }
    public string PhoneNumber { get; set; }
    public string OtpResponse { get; set; }
}
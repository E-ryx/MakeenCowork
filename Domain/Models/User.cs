using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class User
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
    public double WalletBalance{ get; set; }
    public ICollection<Reservation> Reservations{ get; set; }
    public ICollection<Transaction> Transactions{ get; set; }
    public enum GenderType
    {
        Male,
        Female
    }
}
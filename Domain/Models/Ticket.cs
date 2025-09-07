using System.Runtime.InteropServices.JavaScript;

namespace Domain.Models;

public class Ticket
{
    private static readonly Random _random = new Random();

    public Ticket()
    {
        
    }

    public Ticket(int userId)
    {
        UserId = userId;
        TicketNumber = GenerateTicketNumber();
        CreatedAt = DateTime.Now;
    }

    public void AssignAdminToTicket(Ticket ticket, int adminId)
    {
        ticket.AdminId = adminId;
    }
    public int TicketId { get; private set; }
    public int UserId { get; private set; }
    public int? AdminId { get; private set; }
    public string TicketNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ICollection<TicketMessage> Message { get; set; }

    public User User { get; set; }
    
    private string GenerateTicketNumber()
    {
        // Generate a random 8-digit ticket number
        return _random.Next(10000000, 100000000).ToString();
    }
}
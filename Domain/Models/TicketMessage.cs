namespace Domain.Models;

public class TicketMessage
{
    public TicketMessage()
    {
        
    }

    public TicketMessage(int ticketId, int userId, string body)
    {
        TicketId = ticketId;
        SenderUserId = userId;
        Body = body;
        SentAt = DateTimeOffset.UtcNow;
    }
    public int TicketMessageId { get; private set; }
    public int TicketId { get; private set; }
    public int SenderUserId { get; private set; }
    public string Body { get; private set; } 
    public DateTimeOffset SentAt { get; private set; } 
}
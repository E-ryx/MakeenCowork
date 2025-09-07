namespace Domain.DTOs;

public class TicketMessageDto
{
    public int TicketMessageId { get; set; }
    public int TicketId { get; set; }
    public int SenderUserId { get; set; }
    public string Body { get; set; }
    public DateTimeOffset SentAt { get; set; }
}
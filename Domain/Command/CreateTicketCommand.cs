using MediatR;

namespace Domain.Command;

public class CreateTicketCommand: IRequest
{
    public int UserId { get; set; }
}
using MediatR;

namespace Domain.Command;

public class AddTransactionRequestCommand: IRequest<string>
{
    public int UserId { get; set; }
    public int Price { get; set; }
    public string ImageUrl { get; set; }
}
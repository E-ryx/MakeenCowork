using MediatR;

namespace Domain.Command;

public class AddSpaceCommand:IRequest
{
    public string Name { get;  set; }
    public int Capacity { get;  set; }
    public int Price { get;  set; }
    // public int? ImageId { get; private set; }
    public string ExtraServices { get;  set; }

}
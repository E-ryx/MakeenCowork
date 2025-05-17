using Domain.Command;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers;

public class AddSpaceCommandHandler : IRequestHandler<AddSpaceCommand>
{

    private readonly ISpaceRepository _repository;

    public AddSpaceCommandHandler(ISpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddSpaceCommand request, CancellationToken cancellationToken)
    {
        await _repository.AddSpace(request);
    }
}
using Domain.Command;

namespace Domain.Interfaces;

public interface ISpaceRepository
{
    Task AddSpace(AddSpaceCommand addSpaceCommand);
    Task<double> GetPriceOfSpace(int SpaceId);
    Task<int> SpaceIsFreeAtDate(int SpaceId, DateOnly dateOnly);

}
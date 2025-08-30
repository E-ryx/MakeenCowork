using Domain.Command;

namespace Domain.Interfaces;

public interface ISpaceRepository
{
    Task AddSpace(AddSpaceCommand addSpaceCommand);
    Task<int> GetPriceOfSpace(int SpaceId);
    Task<int> SpaceIsFreeAtDate(int SpaceId, DateOnly dateOnly);

}
using Data.Context;
using Domain.Command;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class SpaceRepository:ISpaceRepository
{
    private readonly MyDbContext _context;

    public SpaceRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task AddSpace(AddSpaceCommand addSpaceCommand)
    {
        var space = new Space(addSpaceCommand.Name,addSpaceCommand.Capacity,addSpaceCommand.Price,addSpaceCommand.ExtraServices);
        await _context.AddAsync(space);
        await _context.SaveChangesAsync();
    }

    public async Task<double> GetPriceOfSpace(int SpaceId)
    {
        return await _context.Spaces.Where(a => a.SpaceId == SpaceId).Select(a => a.Price).FirstOrDefaultAsync();
    }
    
    public async Task<int> SpaceIsFreeAtDate(int SpaceId,DateOnly Date)
    {

        var CountOfReservation = await _context.Spaces.Where(a => a.SpaceId == SpaceId).Select(a => a.Capacity).FirstOrDefaultAsync()- await _context.ReservationDays.CountAsync(a => a.SpaceId == SpaceId && a.Date == Date);

        return CountOfReservation;
    }
}
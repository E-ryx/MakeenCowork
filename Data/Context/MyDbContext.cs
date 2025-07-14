using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class MyDbContext:DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users{ get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationDay> ReservationDays { get; set; }
    public DbSet<Space> Spaces { get; set; }
}
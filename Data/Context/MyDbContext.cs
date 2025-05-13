using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class MyDbContext:DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext>options):base(options)
    {
        
    }

    public DbSet<User> Users{ get; set; }
}
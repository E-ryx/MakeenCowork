using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Space
{
    [Key]
    public int SpaceId { get; private set; }
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public double Price { get; private set; }
    // public int? ImageId { get; private set; }
    public string ExtraServices { get; private set; }

    // Navigation Property
    // public List<Reservation> Reservations { get; private set; } = new();

    private Space() { } // EF Core

    public Space(string name, int capacity, int price, string extraServices)
    {
        Name = name;
        Capacity = capacity;
        Price = price;
        ExtraServices = extraServices;
    }
}

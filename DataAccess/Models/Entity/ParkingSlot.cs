using System.ComponentModel;

namespace DataAccess.Models.Entity;

public class ParkingSlot
{
    public int Id { get; set; }

    public int Size { get; set; }
    [DefaultValue(false)]
    public bool IsOccupied { get; set; } = false;
}

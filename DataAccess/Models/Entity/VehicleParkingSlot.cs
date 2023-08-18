namespace DataAccess.Models.Entity;

public class VehicleParkingSlot
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int ParkingSlotId { get; set; }

    public DateTime EntryDateTime { get; set; }

    public DateTime? ExitDateTime { get; set; }
}

namespace DataAccess.Models.Entity;

public class ParkingSlotEntryPointDistance
{
    public int Id { get; set; }

    public int ParkingSlotId { get; set; }

    public int EntryPointId { get; set; }

    public int Distance { get; set; }
}

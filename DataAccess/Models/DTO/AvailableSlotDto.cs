using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.DTO;
public class AvailableSlotDto
{
    public int ParkingSlotId { get; set; }
    public int EntryPointId { get; set; }
    public string EntryPointName { get; set; }
    public int DistanceId { get; set; }
    public int Distance { get; set; }
    public int VehicleSize { get; set; }
    public string VehicleSizeName { get; set; }
    public int SlotSize { get; set; }
    public string SlotSizeName { get; set; }
    public bool IsOccupied { get; set; }
    public string Coordinates { get; set; }
}

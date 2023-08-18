using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.DTO;
public class ExtendedParkingInfoDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int ParkingSlotId { get; set; }
    public DateTime EntryDateTime { get; set; }
    public DateTime? ExitDateTime { get; set; }
    public string PlateNumber { get; set; }
    public int VehicleSize { get; set; }
    public string VehicleSizeName { get; set; }
    public bool IsOccupied { get; set; }
    public int SlotSize { get; set; }
    public string SlotSizeName { get; set; }
    public string Coordinates { get; set; }
}

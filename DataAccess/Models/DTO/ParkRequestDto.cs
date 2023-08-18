using System.ComponentModel;

namespace DataAccess.Models.DTO;
public class ParkRequestDto
{
    [DefaultValue("AAA")]
    public string PlateNumber { get; set; }
    [DefaultValue(1)]
    public int Size { get; set; }
    [DefaultValue("A")]
    public string EntryPoint { get; set; }
    public DateTime EntryDateTime { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Entity;

public class Vehicle
{

    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string PlateNumber { get; set; }

    public int Size { get; set; }
}

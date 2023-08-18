using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Entity;

public class EntryPoint
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }
}

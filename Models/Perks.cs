using System.ComponentModel.DataAnnotations;

namespace EventService.Models;

public class Perk
{
    public int Id { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public int PackageId { get; set; }

    public Package Package { get; set; } = null!;
}

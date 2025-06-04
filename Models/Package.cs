using EventService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Package
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public ICollection<Perk> Perks { get; set; } = new List<Perk>();

    [Required]
    public int EventId { get; set; }

    [ForeignKey("EventId")]
    public Event Event { get; set; } = null!;
}

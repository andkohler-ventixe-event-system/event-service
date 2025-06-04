using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventService.Models;

public class Event
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string ShortDescription { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TicketsMax { get; set; }

    [Required]
    public int TicketsLeft { get; set; }

    [Required]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; } = null!;

    [Required]
    public string ThumbnailImage { get; set; } = null!;

    [Required]
    public string BannerImage { get; set; } = null!;

    [Required]
    public ICollection<Package> Packages { get; set; } = new List<Package>();
}

using System.ComponentModel.DataAnnotations;

public class TicketUpdateRequest
{
    [Required]
    public int Quantity { get; set; }
}
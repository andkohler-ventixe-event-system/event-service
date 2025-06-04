using EventService.Data;
using EventService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
    {
        return await _context.Events
            .Include(e => e.Packages)
            .ThenInclude(p => p.Perks)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var ev = await _context.Events
            .Include(e => e.Packages)
            .ThenInclude(p => p.Perks)
            .FirstOrDefaultAsync(e => e.Id == id);

        return ev == null ? NotFound() : ev;
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event newEvent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
    {
        if (id != updatedEvent.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Entry(updatedEvent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Events.Any(e => e.Id == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null)
            return NotFound();

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/decrease-tickets")]
    public async Task<IActionResult> DecreaseTickets(int id, [FromBody] TicketUpdateRequest request)
    {
        var evnt = await _context.Events.FindAsync(id);
        if (evnt == null)
            return NotFound();

        if (request.Quantity <= 0)
            return BadRequest("Quantity must be greater than 0.");

        if (evnt.TicketsLeft < request.Quantity)
            return BadRequest("Not enough tickets left.");

        evnt.TicketsLeft -= request.Quantity;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

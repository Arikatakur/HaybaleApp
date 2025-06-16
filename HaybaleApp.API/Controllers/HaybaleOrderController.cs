using HaybaleApp.Core.Entities;
using HaybaleApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HaybaleOrderController : ControllerBase
{
    private readonly HaybaleDbContext _context;

    public HaybaleOrderController(HaybaleDbContext context)
    {
        _context = context;
    }

    // GET: api/haybaleorder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HaybaleOrder>>> GetOrders()
    {
        return await _context.HaybaleOrders.Include(o => o.Driver).ToListAsync();
    }

    // GET: api/haybaleorder/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HaybaleOrder>> GetOrder(int id)
    {
        var order = await _context.HaybaleOrders.Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound();
        return order;
    }

    // POST: api/haybaleorder
    [HttpPost]
    public async Task<ActionResult<HaybaleOrder>> CreateOrder(HaybaleOrder order)
    {
        _context.HaybaleOrders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // PUT: api/haybaleorder/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, HaybaleOrder updated)
    {
        if (id != updated.Id) return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/haybaleorder/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.HaybaleOrders.FindAsync(id);
        if (order == null) return NotFound();

        _context.HaybaleOrders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

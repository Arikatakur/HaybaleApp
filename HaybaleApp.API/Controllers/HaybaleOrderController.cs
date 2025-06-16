using HaybaleApp.Core.Entities;
using HaybaleApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.API.Controllers;

[Authorize]
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
    [Authorize(Roles = "Driver,Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HaybaleOrder>>> GetOrders()
    {
        return await _context.HaybaleOrders.Include(o => o.Driver).ToListAsync();
    }

    // GET: api/haybaleorder/5
    [Authorize(Roles = "Driver,Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<HaybaleOrder>> GetOrder(int id)
    {
        var order = await _context.HaybaleOrders.Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound();
        return order;
    }

    // POST: api/haybaleorder
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<HaybaleOrder>> CreateOrder(HaybaleOrder order)
    {
        _context.HaybaleOrders.Add(order);
        await _context.SaveChangesAsync();
        await LogChange("add", "HaybaleOrder", "Full Order", null, System.Text.Json.JsonSerializer.Serialize(order));
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // PUT: api/haybaleorder/5
    [Authorize(Roles = "Driver,Admin")]
    [HttpPut("{id}")]
public async Task<IActionResult> UpdateOrder(int id, HaybaleOrder updated)
{
    if (id != updated.Id) return BadRequest();

    var existing = await _context.HaybaleOrders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    if (existing == null) return NotFound();

    // Log each field change
    if (existing.Price != updated.Price)
        await LogChange("edit", "HaybaleOrder", "Price", existing.Price.ToString(), updated.Price.ToString());

    if (existing.Status != updated.Status)
        await LogChange("edit", "HaybaleOrder", "Status", existing.Status, updated.Status);

    if (existing.Description != updated.Description)
        await LogChange("edit", "HaybaleOrder", "Description", existing.Description, updated.Description);

    if (existing.Weight != updated.Weight)
        await LogChange("edit", "HaybaleOrder", "Weight", existing.Weight.ToString(), updated.Weight.ToString());

    if (existing.DeliveryTime != updated.DeliveryTime)
        await LogChange("edit", "HaybaleOrder", "DeliveryTime", existing.DeliveryTime.ToString("s"), updated.DeliveryTime.ToString("s"));

    if (existing.DriverId != updated.DriverId)
        await LogChange("edit", "HaybaleOrder", "DriverId", existing.DriverId.ToString(), updated.DriverId.ToString());

    // Apply update
    _context.Entry(updated).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}


    // DELETE: api/haybaleorder/5
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.HaybaleOrders.FindAsync(id);
        if (order == null) return NotFound();

        _context.HaybaleOrders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    private async Task LogChange(string action, string entity, string field, string? oldValue, string? newValue, string? notes = null)
    {
        var log = new ChangeLog
        {
            Username = User?.Identity?.Name ?? "System",
            Action = action,
            TargetEntity = entity,
            FieldChanged = field,
            OldValue = oldValue,
            NewValue = newValue,
            Notes = notes,
            Timestamp = DateTime.UtcNow
        };

        _context.ChangeLogs.Add(log);
        await _context.SaveChangesAsync();
    }

}

using HaybaleApp.Core.Entities;
using HaybaleApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly HaybaleDbContext _context;

    public DriverController(HaybaleDbContext context)
    {
        _context = context;
    }

    // GET: api/driver
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
    {
        return await _context.Drivers.ToListAsync();
    }

    // GET: api/driver/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Driver>> GetDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null)
            return NotFound();
        return driver;
    }

    // POST: api/driver
    [HttpPost]
    public async Task<ActionResult<Driver>> CreateDriver(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
    }

    // PUT: api/driver/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver(int id, Driver updated)
    {
        if (id != updated.Id)
            return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/driver/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null)
            return NotFound();

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

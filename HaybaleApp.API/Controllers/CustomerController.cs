using HaybaleApp.Core.Entities;
using HaybaleApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly HaybaleDbContext _context;

    public CustomerController(HaybaleDbContext context)
    {
        _context = context;
    }

    // GET: api/customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.Include(c => c.Orders).ToListAsync();
    }

    // GET: api/customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _context.Customers.Include(c => c.Orders)
                                               .FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null)
            return NotFound();

        return customer;
    }
}

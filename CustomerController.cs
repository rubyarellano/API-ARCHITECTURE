using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;

namespace MovieRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MovieRentalDbContext _context;

        public CustomerController(MovieRentalDbContext context)
        {
            _context = context;
        }
    
    
        [HttpPost]
        public async Task<ActionResult<Customers>> PostMovie(Customers customers)
        {
            _context.Customer.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { name = customers. CustomerName}, customers);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomer()
        {
            if (_context.Customer == null)
            {

                return NotFound();
            }
            return await _context.Customer.ToListAsync();
        }


        [HttpPut]
        public async Task<ActionResult> PutCustomer(string name, Customers customers)
        {
            if (name != customers. CustomerName)
            {
                return BadRequest();
            }
            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersAvailable(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }
        private bool CustomersAvailable(string name)
        {
            return (_context.Customer?.Any(x => x.CustomerName == name)).GetValueOrDefault();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomers(int id)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customers= await _context.Customer.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            _context.Customer.Remove(customers);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}



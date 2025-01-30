using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;

namespace MovieRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly MovieRentalDbContext _context;

        public RentalController(MovieRentalDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<RentalHeader>> PostRentalHeader(RentalHeader rentalHeader)
        {
            rentalHeader.RentalDate = DateTime.Now;
            _context.RentalHeader.Add(rentalHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentalHeader), new { id = rentalHeader.RentalId }, rentalHeader);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalHeader>>> GetRentalHeader()
        {
            if (_context.RentalHeader == null)
            {

                return NotFound();
            }
            return await _context.RentalHeader.ToListAsync();
        }


        [HttpPut]
        public async Task<ActionResult> PutRentalHeader(int id, RentalHeader rentalHeader)
        {
            if (id != rentalHeader.RentalId)
            {
                return BadRequest();
            }
            _context.Entry(rentalHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalHeaderAvailable(id))
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
        private bool RentalHeaderAvailable(int id)
        {
            return (_context.RentalHeader?.Any(x => x.RentalId == id)).GetValueOrDefault();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalHeader>> GetRentalHeader(int id)
        {
            {
                return NotFound();
            }
            var rentalHeader = await _context.RentalHeader.FindAsync(id);
            if (rentalHeader == null)
            {
                return NotFound();

            }
            return rentalHeader;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.RentalHeader == null)
            {
                return NotFound();
            }
            var rentalHeader = await _context.RentalHeader.FindAsync(id);
            if (rentalHeader == null)
            {
                return NotFound();
            }
            _context.RentalHeader.Remove(rentalHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;

namespace MovieRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieRentalDbContext _context;

        public MovieController(MovieRentalDbContext context)
        {
            _context = context;
        }
     

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_context.Movies == null)
            {

                return NotFound();
            }
            return await _context.Movies.ToListAsync();
        }


        [HttpPut] 
        public async Task<ActionResult> PutMovie(int id, Movie movie)
        { 
            if (id != movie.MovieId)
            { 
                return BadRequest(); 
             }
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovievAvailable(id))
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
        private bool MovievAvailable(int id)
        {
            return(_context.Movies?.Any(x=>x.MovieId == id)).GetValueOrDefault();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var Movie = await _context.Movies.FindAsync(id);
            if (Movie == null)
            {
                return NotFound();

            }
            return Movie;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound(); 
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

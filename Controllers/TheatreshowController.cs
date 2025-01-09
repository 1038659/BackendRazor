using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace BackendRazor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheatreShowController : ControllerBase
    {
        private readonly DatabaseContext _context; // Changed from ApplicationDbContext

        public TheatreShowController(DatabaseContext context) // Changed from ApplicationDbContext
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllShows()
        {
            var shows = _context.TheatreShow
                .Select(show => new
                {
                    show.TheatreShowId,
                    show.Title,
                    show.Description,
                    show.Price,
                    Venue = new
                    {
                        show.Venue.VenueId,
                        show.Venue.Name,
                        show.Venue.Capacity
                    },
                    Dates = show.theatreShowDates.Select(date => date.DateAndTime)
                })
                .ToList();

            return Ok(shows);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateShow([FromBody] TheatreShowDto showDto)
        {
            var venue = _context.Venue.FirstOrDefault(v => v.VenueId == showDto.VenueId) ?? new Venue
            {
                Name = showDto.VenueName,
                Capacity = showDto.VenueCapacity
            };

            var show = new TheatreShow
            {
                Title = showDto.Title,
                Description = showDto.Description,
                Price = showDto.Price,
                Venue = venue,
                theatreShowDates = showDto.Dates.Select(date => new TheatreShowDate { DateAndTime = date }).ToList()
            };

            _context.TheatreShow.Add(show);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAllShows), new { id = show.TheatreShowId }, show);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateShow(int id, [FromBody] TheatreShowDto showDto)
        {
            var show = _context.TheatreShow.Include(s => s.theatreShowDates).FirstOrDefault(s => s.TheatreShowId == id);
            if (show == null)
            {
                return NotFound();
            }

            show.Title = showDto.Title;
            show.Description = showDto.Description;
            show.Price = showDto.Price;

            var venue = _context.Venue.FirstOrDefault(v => v.VenueId == showDto.VenueId) ?? new Venue
            {
                Name = showDto.VenueName,
                Capacity = showDto.VenueCapacity
            };
            show.Venue = venue;

            show.theatreShowDates.Clear();
            show.theatreShowDates.AddRange(showDto.Dates.Select(date => new TheatreShowDate { DateAndTime = date }));

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteShow(int id)
        {
            var show = _context.TheatreShow.FirstOrDefault(s => s.TheatreShowId == id);
            if (show == null)
            {
                return NotFound();
            }

            _context.TheatreShow.Remove(show);
            _context.SaveChanges();

            return NoContent();
        }
    }

    public class TheatreShowDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public int VenueCapacity { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
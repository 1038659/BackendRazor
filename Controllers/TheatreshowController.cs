using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/theatre-show")]
    public class TheatreShowController : ControllerBase
    {
        private readonly DatabaseContext _context; // Changed from ApplicationDbContext
        private readonly IAuthService authService;

        public TheatreShowController(IAuthService authService, DatabaseContext context) // Changed from ApplicationDbContext
        {
            this.authService = authService;
            this._context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetShows()
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
        [HttpGet("{id?}")]
        [AllowAnonymous]
        public IActionResult GetShows(int? id, string title = null, string description = null, string location = null, DateTime? startDate = null, DateTime? endDate = null, string sortBy = null, bool ascending = true)
        {
            if (id.HasValue)
            {
            var show = _context.TheatreShow
                .Where(s => s.TheatreShowId == id.Value)
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
                .FirstOrDefault();

            if (show == null)
            {
                return NotFound();
            }

            return Ok(show);
            }

            var showsQuery = _context.TheatreShow.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
            showsQuery = showsQuery.Where(s => s.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(description))
            {
            showsQuery = showsQuery.Where(s => s.Description.Contains(description));
            }

            if (!string.IsNullOrEmpty(location))
            {
            showsQuery = showsQuery.Where(s => s.Venue.Name.Contains(location));
            }

            if (startDate.HasValue)
            {
            showsQuery = showsQuery.Where(s => s.theatreShowDates.Any(d => d.DateAndTime >= startDate.Value));
            }

            if (endDate.HasValue)
            {
            showsQuery = showsQuery.Where(s => s.theatreShowDates.Any(d => d.DateAndTime <= endDate.Value));
            }

            switch (sortBy?.ToLower())
            {
            case "title":
                showsQuery = ascending ? showsQuery.OrderBy(s => s.Title) : showsQuery.OrderByDescending(s => s.Title);
                break;
            case "price":
                showsQuery = ascending ? showsQuery.OrderBy(s => s.Price) : showsQuery.OrderByDescending(s => s.Price);
                break;
            case "date":
                showsQuery = ascending ? showsQuery.OrderBy(s => s.theatreShowDates.Min(d => d.DateAndTime)) : showsQuery.OrderByDescending(s => s.theatreShowDates.Max(d => d.DateAndTime));
                break;
            default:
                showsQuery = ascending ? showsQuery.OrderBy(s => s.Title) : showsQuery.OrderByDescending(s => s.Title);
                break;
            }

            var shows = showsQuery
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

        [HttpPost("create")]
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

            return CreatedAtAction(nameof(GetShows), new { id = show.TheatreShowId }, show);
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
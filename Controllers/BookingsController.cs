using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineWebAPI.Models;
using AilrineWebAPI.Repository.BookingsRepository;
using Microsoft.Extensions.Logging;

namespace AirlineWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly AirlineDbContext _context;
        private readonly ILogger<BookingsController> _logger;
        private readonly IBookingsRepository _bookingsRepository;

        public BookingsController(AirlineDbContext context, ILogger<BookingsController> logger,
            IBookingsRepository bookingsRepository)
        {
            _context = context;
            _logger = logger;
            _bookingsRepository = bookingsRepository;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Getbookings()
        {
            //_logger.LogInformation("Getting all the bookings successfully.");
            //return await _context.bookings.ToListAsync();
            return await _bookingsRepository.Getbookings();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            //var booking = await _context.bookings.FindAsync(id);

            //if (booking == null)
            //{
            //    _logger.LogError("Sorry, no booking found with this id " + id);
            //    return NotFound();
            //}
            //_logger.LogInformation("Getting all the flights successfully.");
            //return booking;
            try
            {
                return await _bookingsRepository.GetBooking(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            //_context.bookings.Add(booking);
            //await _context.SaveChangesAsync();
            //_logger.LogInformation("Booking created successfully.");

            //return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
            await _bookingsRepository.PostBooking(booking);

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            //var booking = await _context.bookings.FindAsync(id);
            //if (booking == null)
            //{
            //    return NotFound();
            //}

            //_context.bookings.Remove(booking);
            //await _context.SaveChangesAsync();
            //_logger.LogInformation("Booking deleted successfully.");

            //return booking;
            try
            {
                return await _bookingsRepository.DeleteBooking(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        private bool BookingExists(int id)
        {
            //return _context.bookings.Any(e => e.BookingId == id);
            return _bookingsRepository.BookingExists(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineWebAPI.Models;
using AilrineWebAPI.Repository.FlightsRepository;
using Microsoft.Extensions.Logging;

namespace AirlineWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightsRepository _flightsRepository;

        public FlightsController(AirlineDbContext context, ILogger<FlightsController> logger,
            IFlightsRepository flightsRepository)
        {
            _context = context;
            _logger = logger;
            _flightsRepository = flightsRepository;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> Getflights()
        {
            //_logger.LogInformation("Getting all the flights successfully.");
            //return await _context.flights.ToListAsync();
            return await _flightsRepository.Getflights();
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            //var flight = await _context.flights.FindAsync(id);

            //if (flight == null)
            //{
            //    _logger.LogError("Sorry, no flight found with this id " + id);
            //    return NotFound();
            //}

            //return flight;
            try
            {
                return await _flightsRepository.GetFlight(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        // Searching a flight with source and destination
        // GET: api/Flights/source/destination
        [HttpGet("{source}/{destination}")]
        public async Task<ActionResult<Flight>> GetFlightBySourceAndDestination(string source, string destination)
        {
            //var flight = await _context.flights.FirstOrDefaultAsync(x => x.Source == source && x.Destination == destination);

            //if (flight == null)
            //{
            //    _logger.LogError("Sorry, unable to find a flight.");
            //    return NotFound();
            //}
            //_logger.LogInformation("Successfully getting a flight with the given params.");
            //return flight;
            try
            {
                return await _flightsRepository.GetFlightBySourceAndDestination(source, destination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightId)
            {
               return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _logger.LogInformation("Flight updated successfully.");

            return NoContent();
        }

        // POST: api/Flights
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            //_context.flights.Add(flight);
            //await _context.SaveChangesAsync();
            //_logger.LogInformation("Flight created successfully.");

            //return CreatedAtAction("GetFlight", new { id = flight.FlightId }, flight);
            await _flightsRepository.PostFlight(flight);
            return CreatedAtAction("GetFlight", new { id = flight.FlightId }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Flight>> DeleteFlight(int id)
        {
            //var flight = await _context.flights.FindAsync(id);
            //if (flight == null)
            //{
            //    return NotFound();
            //}

            //_context.flights.Remove(flight);
            //await _context.SaveChangesAsync();
            //_logger.LogInformation("Flight deleted successfully.");

            //return flight;
            try
            {
                return await _flightsRepository.DeleteFlight(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        private bool FlightExists(int id)
        {
            //return _context.flights.Any(e => e.FlightId == id);
            return _flightsRepository.FlightExists(id);
        }
    }
}

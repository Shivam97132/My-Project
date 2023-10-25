using Microsoft.EntityFrameworkCore;
using AirlineWebAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AirlineWebAPI.Models
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
     : base(options)
        {
        }

        public virtual DbSet<RegisterUser> users { get; set; }
        public virtual DbSet<Flight> flights { get; set; }
        public virtual DbSet<Booking> bookings { get; set; }
       
    }
}

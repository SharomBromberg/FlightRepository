using Microsoft.EntityFrameworkCore;
using FlightAPP.Domain.Entities;

namespace FlightApp.Data.Infrastructure
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<JourneyEntity> Journeys { get; set; }
        public DbSet<FlightEntity> Flights { get; set; }
        public DbSet<TransportEntity> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=flights.db");
            }
        }
    }
}
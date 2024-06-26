using FlightAPP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Data.Infrastructure
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<FlightEntity> Flights { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=flights.db");
            }
        }
    }
}
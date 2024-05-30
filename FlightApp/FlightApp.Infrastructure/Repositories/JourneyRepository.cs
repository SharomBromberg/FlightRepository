using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlightAPP.Domain.Entities;
using FlightApp.Data.Infrastructure;
using FlightApp.Infrastructure.Interfaces;

namespace FlightApp.Infrastructure.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly FlightContext _context;

        public JourneyRepository(FlightContext context)
        {
            _context = context;
        }

        public async Task<List<FlightEntity>> GetFlights()
        {
            return await _context.Flights.ToListAsync();
        }
        public async Task SaveFlights(List<FlightEntity> flights)
        {
            _context.Flights.AddRange(flights);
            await _context.SaveChangesAsync();
        }

    }
}
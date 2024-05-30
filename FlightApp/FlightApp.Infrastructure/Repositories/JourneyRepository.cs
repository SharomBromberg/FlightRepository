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

        public async Task<IEnumerable<JourneyEntity>> GetAllJourneys()
        {
            return await _context.Journeys.ToListAsync();
        }

        public async Task AddJourney(JourneyEntity journey)
        {
            _context.Journeys.Add(journey);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FlightEntity>> GetFlights()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task AddFlight(FlightEntity flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
        }
    }
}
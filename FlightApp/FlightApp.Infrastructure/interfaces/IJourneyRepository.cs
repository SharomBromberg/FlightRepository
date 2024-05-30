
using FlightAPP.Domain.Entities;

namespace FlightApp.Infrastructure.Interfaces
{
    public interface IJourneyRepository
    {
        Task<IEnumerable<JourneyEntity>> GetAllJourneys();
        Task AddJourney(JourneyEntity journey);
    }
}
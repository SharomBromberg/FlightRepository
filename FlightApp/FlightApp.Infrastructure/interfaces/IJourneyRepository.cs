
using FlightAPP.Domain.Entities;

namespace FlightApp.Infrastructure.Interfaces
{
    public interface IJourneyRepository
    {
        Task<List<FlightEntity>> GetFlights();
        Task SaveFlights(List<FlightEntity> flights);
    }
}
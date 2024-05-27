using FlightAPP.Domain.Models;

namespace FlightAPP.Domain.Interfaces;

public interface IJourneyService
{
    Task<IEnumerable<Flight>> GetAllFlights();
    Task<IEnumerable<List<Flight>>> GetOneWayFlights(string origin, string destination, string currency, bool allowStops);
    Task<IEnumerable<List<Flight>>> GetRoundTripFlights(string origin, string destination, string currency, bool allowStops);

}
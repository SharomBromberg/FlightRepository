using FlightAPP.Domain.Models;

namespace FlightAPP.Application.Interfaces;

public interface IJourneyService
{
    Task<IEnumerable<Flight>> GetAllFlights();
    Task<IEnumerable<Journey>> GetOneWayFlights(string origin, string destination, string currency, bool allowStops);
    Task<IEnumerable<Journey>> GetRoundTripFlights(string origin, string destination, string currency, bool allowStops);
}
using FlightAPP.Domain.Models;

namespace FlightAPP.Models;
public class Journey
{
    public required List<Flight> Flights { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public double Price { get; set; }
}
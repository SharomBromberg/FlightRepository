
namespace FlightAPP.Domain.Models;
public class Journey
{

    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public double Price { get; set; }
    public required List<Flight> Flights { get; set; }
}
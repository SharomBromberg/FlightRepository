
using FlightAPP.Domain.Models;
namespace FlightAPP.Domain.Models;

public class Flight
{
    public required Transport Transport { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public double Price { get; set; }
    public Flight Clone()
    {
        return new Flight
        {
            Transport = this.Transport,
            Origin = this.Origin,
            Destination = this.Destination,
            Price = this.Price
        };
    }
}

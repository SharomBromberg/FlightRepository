
namespace FlightAPP.Domain.Entities
{
    public class FlightEntity
    {
        public required string FlightCarrier { get; set; }
        public required string FlightNumber { get; set; }
        public int Id { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public double Price { get; set; }

    }
}

namespace FlightAPP.Domain.Entities
{
    public class JourneyEntity
    {
        public JourneyEntity()
        {
            Flights = new List<FlightEntity>();
        }

        public int Id { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<FlightEntity> Flights { get; set; }
    }
}
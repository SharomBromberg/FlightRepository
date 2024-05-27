namespace FlightAPP.Domain.Entities
{
    public class FlightEntity
    {
        public int Id { get; set; }
        public int? TransportId { get; set; }
        public virtual TransportEntity? Transport { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public double Price { get; set; }

        public virtual ICollection<JourneyEntity> Journeys { get; set; } = new List<JourneyEntity>();
    }
}
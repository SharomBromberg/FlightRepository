namespace FlightAPP.Domain.Entities
{
    public class TransportEntity
    {
        public int Id { get; set; }
        public string FlightCarrier { get; set; } = null!;
        public string FlightNumber { get; set; } = null!;

        public virtual ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();
    }
}
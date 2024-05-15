namespace Quanlybenxe123.Models
{
    public class Bus
    {
        public int BusId { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
        public ICollection<BusTrip> BusTrips { get; set; } = new List<BusTrip>();

    }
}

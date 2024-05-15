namespace Quanlybenxe123.Models
{
    public class Routes
    {
        public int RoutesId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public ICollection<BusTrip> BusTrips { get; set; }
    }
}

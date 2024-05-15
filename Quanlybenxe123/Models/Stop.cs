namespace Quanlybenxe123.Models
{
    public class Stop
    {
        public int StopId { get; set; }
        public string Location { get; set; }
        public int BusTripId { get; set; }
        public BusTrip BusTrip { get; set; }
    }
}

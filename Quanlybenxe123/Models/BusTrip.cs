namespace Quanlybenxe123.Models
{
    public class BusTrip
    {
        public int BusTripId { get; set; }
        public int RouteId { get; set; }
        public Routes Routes { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public ICollection<Booking> Bookings { get; set; } // Danh sách các đặt chỗ trên chuyến đi này
        public ICollection<Stop> Stops { get; set; }
        public ICollection<BusTripImage> BusTripImages { get; set; }
    }
}

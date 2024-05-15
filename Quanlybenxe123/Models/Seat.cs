namespace Quanlybenxe123.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Booking> Bookings { get; set; } // Dan
    }
}

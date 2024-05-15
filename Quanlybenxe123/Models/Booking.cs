using System.ComponentModel.DataAnnotations.Schema;

namespace Quanlybenxe123.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        // Foreign key
        public int BusTripId { get; set; }

        // Navigation property
        public BusTrip BusTrip { get; set; }

        // Other properties
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

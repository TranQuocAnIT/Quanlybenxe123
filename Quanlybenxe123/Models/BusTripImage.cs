namespace Quanlybenxe123.Models
{
    public class BusTripImage
    {
        public int BusTripImageId { get; set; }
        public string ImageUrl { get; set; } // Đường dẫn hình ảnh
        public int BusTripId { get; set; }
        public BusTrip BusTrip { get; set; }
    }
}

namespace JemeHotelsProject.Models.DTOs
{
    public class RoomDTO
    {
        public Guid roomID { get; set; }
        public int roomNumber { get; set; }
        public string roomType { get; set; }
        public decimal price { get; set; }
        public bool isAvailable { get; set; }

    }
}

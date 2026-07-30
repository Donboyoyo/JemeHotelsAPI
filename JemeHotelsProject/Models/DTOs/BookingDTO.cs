namespace JemeHotelsProject.Models.DTOs
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public int numberOfDays { get; set; }
        public decimal totalBill { get; set; }
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }
    }
}

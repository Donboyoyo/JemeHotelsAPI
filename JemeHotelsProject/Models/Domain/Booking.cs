namespace JemeHotelsProject.Models.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public int numberOfDays { get; set; }
        public decimal totalBill { get; set; }
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }




        // Navigation Properties

        public Guest Guests { get; set; }
        public Room Rooms { get; set; }


    }
}

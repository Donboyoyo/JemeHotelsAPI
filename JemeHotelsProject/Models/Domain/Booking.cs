namespace JemeHotelsProject.Models.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int numberOfDays { get; set; }
        public decimal totalBill { get; set; }

        public bool isPaid { get; set; } = false;
        public string GuestId { get; set; }
        public Guid RoomId { get; set; }




        // Navigation Properties

        public Guest Guests { get; set; }
        public Room Rooms { get; set; }


    }
}

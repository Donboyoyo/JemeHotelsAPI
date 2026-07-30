namespace JemeHotelsProject.Models.DTOs
{
    public class returnBookingDetailsDTO
    {
        public Guid Id { get; set; }
        public int numberOfDays { get; set; }
        public decimal totalBill { get; set; }

    }
}

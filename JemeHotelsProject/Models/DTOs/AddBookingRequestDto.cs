using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.DTOs
{
    public class AddBookingRequestDto
    {

        [Required]
        public Guid GuestId { get; set; }
        [Required]
        public Guid RoomId { get; set; }
        public int numberOfDays {  get; set; }
    }
}

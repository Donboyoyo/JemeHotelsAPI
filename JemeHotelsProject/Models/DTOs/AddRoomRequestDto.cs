using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.DTOs
{
    public class AddRoomRequestDto
    {
        [Required]
        [Range(101, 120, ErrorMessage ="Room number must be between 101 and 120")]
        public int roomNumber { get; set; }

        [Required]
        public string roomType { get; set; }
        public decimal price { get; set; }

    }
}

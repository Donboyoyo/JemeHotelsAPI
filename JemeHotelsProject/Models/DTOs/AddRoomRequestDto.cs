using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JemeHotelsProject.Models.DTOs
{
    public class AddRoomRequestDto
    {
        [Required]
        [Range(101, 120, ErrorMessage ="Room number must be between 101 and 120")]
        public int RoomNumber { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public decimal Price { get; set; }

        // This matches the frontend's FILE[] requirement
        public List<IFormFile>? Images { get; set; }
    }
}
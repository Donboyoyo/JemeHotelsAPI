using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.DTOs
{
    public class AddGuestRequestDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage ="The password and confirmation does not match.")]
        public string? confirmPassword {  get; set; } = string.Empty;
    }
}

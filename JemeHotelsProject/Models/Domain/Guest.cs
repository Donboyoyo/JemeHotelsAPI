using System.ComponentModel.DataAnnotations;

namespace JemeHotelsProject.Models.Domain
{
    public class Guest
    {
        public string GuestID { get; set; }
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}

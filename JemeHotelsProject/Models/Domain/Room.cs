using System.ComponentModel.DataAnnotations.Schema;

namespace JemeHotelsProject.Models.Domain
{
    public class Room
    {
        public Guid roomID { get; set; }
        public int roomNumber { get; set; }
        public string roomType { get; set; }
        public decimal price { get; set; }
        public bool isAvailable { get; set; } = true;


    }

   
}

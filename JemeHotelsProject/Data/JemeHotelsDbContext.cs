using JemeHotelsProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JemeHotelsProject.Data
{
    public class JemeHotelsDbContext: DbContext
    {
        public JemeHotelsDbContext(DbContextOptions<JemeHotelsDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Guest> Guests {  get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We would seed the data for Rooms

            var guests = new List<Guest>()
            {
                new Guest()
                {
                    GuestID = Guid.Parse("bedc78e9-661d-469c-b504-cecdfd9f8a4c"),
                    PhoneNo = "08056245835",
                    Name = "Dr. Obasi",
                },

                new Guest()
                {
                    GuestID = Guid.Parse("39d9d260-25e3-4e33-930b-eb18547dc1b3"),
                    PhoneNo = "08079885512",
                    Name = "Ossai",
                }
            };

            modelBuilder.Entity<Guest>().HasData(guests);
        }

    }
}

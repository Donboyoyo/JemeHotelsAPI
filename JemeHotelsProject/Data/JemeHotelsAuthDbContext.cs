using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JemeHotelsProject.Data
{
    public class JemeHotelsAuthDbContext : IdentityDbContext
    {
        public JemeHotelsAuthDbContext(DbContextOptions<JemeHotelsAuthDbContext> options) : base(options)
        { }
            protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminId = "9ccb4928-6d6a-43d9-88f3-43ca2f2b8f6c";
            var userId = "309d3fdf-1064-46fb-96b4-85b071dcd683";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName  = "ADMIN"
                },

                new IdentityRole
                {
                    Id = userId ,
                    ConcurrencyStamp= userId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
    
}

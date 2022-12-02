using Datalayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Datalayer.Context
{
    public class HotelDbContext : IdentityDbContext<User>
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options) { }

        public DbSet<Room>? Rooms { get; set; }
        public DbSet<ImageModel>? Images { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Receipt>? Receipts { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }
    }
}

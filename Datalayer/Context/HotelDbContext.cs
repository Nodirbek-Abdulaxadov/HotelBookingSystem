using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Datalayer.Context
{
    public class HotelDbContext : IdentityDbContext<User>
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options) { }

        public DbSet<RoomType>? RoomTypes { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Receipt>? Receipts { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                        .HasOne(i => i.RoomType)
                        .WithMany();

            modelBuilder.Entity<RoomType>()
                        .HasMany(i => i.Rooms)
                        .WithOne(r => r.RoomType);

            base.OnModelCreating(modelBuilder);
        }
    }
}

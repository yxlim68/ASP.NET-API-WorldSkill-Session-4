using s4.Models.Entities;
using Microsoft.EntityFrameworkCore;
using s4.Model.Entities;

namespace s4.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<Areas> Areas{ get; set; }
        public DbSet<Attractions> Attractions{ get; set; }
        public DbSet<ItemAmenities> ItemAmenities { get; set; }
        public DbSet<ItemAttractions> ItemAttractions{ get; set; }
        public DbSet<BookingDetails> BookingDetails{ get; set; }
        public DbSet<Bookings> Bookings{ get; set; }
        public DbSet<Coupons> Coupons{ get; set; }
        public DbSet<Scores> Scores{ get; set; }
        public DbSet<Transactions> Transactions{ get; set; }
        public DbSet<TransactionTypes> TransactionTypes{ get; set; }
        public DbSet<ItemPrices> ItemPrices{ get; set; }
        public DbSet<ItemScores> ItemScores{ get; set; }
        public DbSet<Items> Items{ get; set; }
        public DbSet<ItemTypes> ItemTypes{ get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserTypes> UserTypes{ get; set; }
    }
}

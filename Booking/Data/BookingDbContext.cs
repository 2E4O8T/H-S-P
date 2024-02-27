using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data
{
    public class BookingDbContext :DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext>options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Consultant>();
        }

        public DbSet<Consultant> Consultants { get; set; }
    }
}

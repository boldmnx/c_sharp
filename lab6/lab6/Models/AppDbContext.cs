using Microsoft.EntityFrameworkCore;

namespace lab6.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bagsh> BagshNar { get; set; }
        public DbSet<ErdmiinZereg> ErdmiinZereguud { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bagsh>()
                .OwnsOne(b => b.info);
            modelBuilder.Entity<ErdmiinZereg>()
           .OwnsOne(b => b.Info);
        }


    }
}

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
                .OwnsOne(b => b.info, bInfo =>
                {
                    bInfo.Property(i => i.CreatedDate).IsRequired(); // IsRequired() ашиглаж байна
                });

            base.OnModelCreating(modelBuilder);
        }


    }
}

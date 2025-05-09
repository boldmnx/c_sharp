using delguur.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace delguur.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CeramicProduct> CeramicProducts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 
    }
}

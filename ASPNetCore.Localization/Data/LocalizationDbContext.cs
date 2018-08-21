using ASPNetCore.Localization.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCore.Localization.Data
{
    public class LocalizationDbContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

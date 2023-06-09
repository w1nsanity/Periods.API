using Periods.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Periods.API.Data
{
    public class PeriodsDbContext : DbContext
    {
        public PeriodsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Period>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }

        //Dbset
        public DbSet<Period> Periods { get; set; }
    }
}

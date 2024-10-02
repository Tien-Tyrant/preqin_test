using Microsoft.EntityFrameworkCore;
using PreqinAPI.Models;

namespace PreqinAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Investor> Investors { get; set; }
        public DbSet<AssetClass> AssetClasses { get; set; }
        public DbSet<Commitment> Commitments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investor>()
                .HasMany(i => i.Commitments)
                .WithOne(c => c.Investor)
                .HasForeignKey(c => c.InvestorId);

            modelBuilder.Entity<AssetClass>()
                .HasMany(a => a.Commitments)
                .WithOne(c => c.AssetClass)
                .HasForeignKey(c => c.AssetClassId);
        }
    }
}

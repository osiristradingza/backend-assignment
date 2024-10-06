using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace OT.Assessment.App.Models.Casino
{
    public class CasinoContext : DbContext
    {
        public CasinoContext(DbContextOptions<CasinoContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CasinoWagers>().ToTable("CasinoWagers");
            modelBuilder.Entity<CasinoWagers>().Property(c => c.Amount).HasColumnType("decimal(18, 2)");
        }
        public DbSet<Casino.CasinoWagers> CasinoWagers { get; set; }
        public DbSet<Casino.Players> Players { get; set; }
    }
}

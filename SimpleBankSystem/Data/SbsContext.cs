using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Models.Entity;

namespace SimpleBankSystem.Data
{
    public class SbsContext : DbContext
    {
        public SbsContext(DbContextOptions<SbsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new AccountMap(modelBuilder.Entity<Account>());
        }

        public DbSet<Account> Accounts { get; set; }
    }
}

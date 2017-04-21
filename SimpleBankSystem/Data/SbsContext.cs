using Microsoft.EntityFrameworkCore;
using SimpleBankSystem.Models.Entity;

namespace SimpleBankSystem.Data
{
    public class SbsContext : DbContext
    {
        public SbsContext(DbContextOptions<SbsContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}

using AMAPG4.Models.User;
using Microsoft.EntityFrameworkCore;

namespace AMAPG4.Models
{
    public class MyDBContext : DbContext
    {
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<CE> CEs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=!AmapG4P2;database=AmapG4");
        }
    }
}

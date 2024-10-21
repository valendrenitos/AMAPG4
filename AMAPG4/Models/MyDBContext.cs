using AMAPG4.Models.User;
using AMAPG4.Models.Catalog;
using AMAPG4.Models.Command;
using Microsoft.EntityFrameworkCore;
using AMAPG4.Models.Command;
using AMAPG4.Models.ContactForm;

namespace AMAPG4.Models
{
    public class MyDBContext : DbContext
    {
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<CE> CEs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<NewProduct> NewProducts { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CommandLine> CommandLines { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=!AmapG4P2;database=AmapG4");
        }
    }
}

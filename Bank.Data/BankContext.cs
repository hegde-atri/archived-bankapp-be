using System.Dynamic;
using Bank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class BankContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=BankAppDb");
            base.OnConfiguring(optionsBuilder);
        }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }
    }
}

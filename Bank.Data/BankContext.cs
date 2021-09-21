﻿using System.Dynamic;
using System.Linq;
using Bank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    
    /*
     This class is used to query and save data to the database. It also contains the connection string to the db.
     Inherits from the DbContext class
     */
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
            // Here we choose our data source.
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=BankAppDb");
            base.OnConfiguring(optionsBuilder);
        }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}

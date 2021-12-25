using System;
using System.Collections.Generic;

namespace Bank.Data.Entities
{
  public class Customer
  {
    // These are the attributes each customer should have
    public int CustomerId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public DateTime DoB { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    
    //These define the Customer's relation with other Tables.
    public ICollection<Account> Accounts { get; set; }
    public ICollection<Payee> Payees { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    
  }
}
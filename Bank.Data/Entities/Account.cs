﻿using System;
using System.Collections.Generic;

namespace Bank.Data.Entities
{
  public class Account
  {
    //These are the attributes of the Account Table.
    public int AccountId { get; set; }
    //Each account belongs to a single customer. [CustomerId is a foreign key]
    public int CustomerId { get; set; }
    public string AccountNumber { get; set; }
    public string Sortcode { get; set; }
    public string Type { get; set; }
    public decimal Balance { get; set; }
    public string Status { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    /*  These are the relationships that the Account table has other than the customer
        Each Account can have many transactions  */
    public ICollection<Transaction> Transactions { get; set; }
  }
}

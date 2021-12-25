using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Data.Entities
{
  public class Transaction
  {
    //here although i am using using a transaction type =, i.e. either credit or debit
    //I will still keep the amount a signed integer to make it easier to update the balance of an account
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount{ get; set; }
    public string Description { get; set; }
    public DateTime TransDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
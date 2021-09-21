using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Data.Entities
{
  public class Transaction
  {
    public int TransactionId { get; set; }
    public string AccountNumber { get; set; }
    public string Type { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount{ get; set; }
    public string Description { get; set; }
    public DateTime TransDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
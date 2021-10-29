using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.API.Models
{
  public class TransferModel
  {
    
    [Required]
    public string Account1AccountNumber { get; set; }
    [Required]
    public string Account1Type { get; set; }
    public string Account1Description { get; set; }
    public DateTime Account1TransDateTime { get; set; }
    public string Account1CreatedBy { get; set; }
    public DateTime Account1CreatedDate { get; set; }
    
    [Required]
    public string Account2AccountNumber { get; set; }
    [Required]
    public string Account2Type { get; set; }
    public string Account2Description { get; set; }
    public DateTime Account2TransDateTime { get; set; }
    public string Account2CreatedBy { get; set; }
    public DateTime Account2CreatedDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
  }
}
using System;

namespace Bank.Data.Entities
{
  public class Notification
  {
    public int NotificationId { get; set; }
    public int CustomerId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Preference { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    
  }
}
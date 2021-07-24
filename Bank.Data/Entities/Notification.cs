﻿using System;

namespace Bank.Data.Entities
{
  public class Notification
  {
    public int NotificationId { get; set; }
    public int CustomerId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    // Preference is whether they want the notification to email, phone or both.
    public string Preference { get; set; }
    
    // Type indicates whether the notificationId set for the customer
    // is primary,secondary or tertiary.
    public string Type { get; set; }
    // This is for whether they want updates sent to this notification.
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    
  }
}
using System;

namespace Bank.Data.Entities
{
  public class Address
  {
    public int AddressId { get; set; }
    public int CustomerId { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Postcode { get; set; }
    /* The customer Id, and the type of address (either primary or secondary)
      will be used for api calls. */
    public string Type { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
  }
}

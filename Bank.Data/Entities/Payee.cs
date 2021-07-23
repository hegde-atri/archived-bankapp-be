namespace Bank.Data.Entities
{
  public class Payee
  {
    //Attributes of the Payee Table
    public int PayeeId { get; set; }
    //Each account belongs to a single customer. [CustomerId is a foreign key]
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string Sortcode { get; set; }
    public string Description { get; set; }
    
  }
}
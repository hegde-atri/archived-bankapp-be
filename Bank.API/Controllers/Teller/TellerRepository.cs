using System.Threading.Tasks;
using Bank.API.Controllers.Customer;
using Bank.Data;
using Microsoft.Extensions.Logging;

namespace Bank.API.Controllers.Teller
{
  /* This repository is an implementation of ITellerRepository
   All it needs to do is be able to add a transaction
   
   !! Although an abstract layer of the teller repository seems redundant I still made one to stick with
      way my code is structured throughout this project !!
   */
  public class TellerRepository: ITellerRepository
  {
    private readonly BankContext _context;
    private readonly ILogger<CustomerRepository> _logger;

    public TellerRepository(BankContext context, ILogger<CustomerRepository> logger)
    {
      _context = context;
      _logger = logger;
    }
    
    public void Add<T>(T entity) where T : class
    {
      _logger.LogInformation($"Adding object of type {entity.GetType()} to the context");
      _context.Add(entity);
    }

    //Debit is when you deposit money.

    //Credit is when you withdraw money.

    public async Task<bool> SaveChangesAsync()
    {
      _logger.LogInformation("Attempting to save changes to the context");
      // Returns true if more than one line was changed
      // The change is tracked by the DbContext of entity framework
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}
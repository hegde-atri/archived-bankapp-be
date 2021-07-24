using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAPI.Controllers.Customer
{
  public class CustomerRepository: ICustomerRepository
  {
    private readonly BankContext _context;
    private readonly ILogger<CustomerRepository> _logger;

    // Implementation of ICustomerRepository
    public CustomerRepository(BankContext context, ILogger<CustomerRepository> logger)
    {
      _context = context;
      _logger = logger;
    }

    public void Add<T>(T entity) where T : class
    {
      _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
      _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      _logger.LogInformation("Attempting to save changes in the context");
      // The following return statement will return true only if at least a row of data was changed.
      return (await _context.SaveChangesAsync()) > 0;
    }

    public Task<Account> GetAccountAsync(int accountId, int customerId, bool onlyActive = true)
    {
      _logger.LogInformation($"Getting an account for {accountId}");
      throw new System.NotImplementedException();
    }

    public Task<Account[]> GetAllAccountsAsync(int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public Task<Account> GetAddressAsync(int addressId, int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public Task<Account[]> GetAllAddressesAsync(int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Notification> GetNotificationAsync(string type, int customerId, bool onlyActive = true)
    {
      _logger.LogInformation($"Getting notification details of customer {customerId} with type {type}.");

      IQueryable<Notification> query = _context.Notifications
        .Where(n => n.Type == type && n.CustomerId == customerId);

      if (onlyActive)
      {
        query = query.Where(c => c.Status == "active");
      }

      return await query.FirstOrDefaultAsync();
    }

    public Task<Notification[]> GetAllNotificationsAsync(int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public Task<Account> GetPayeeAsync(string name, int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public Task<Account[]> GetAllPayeesAsync(int customerId, bool onlyActive = true)
    {
      throw new System.NotImplementedException();
    }

    public Task<Transaction> GetTransactionAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task<Transaction> GetTransactionsForAccountsAsync(Account[] accounts)
    {
      throw new System.NotImplementedException();
    }
  }
}
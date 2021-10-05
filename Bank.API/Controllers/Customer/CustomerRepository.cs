using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bank.API.Controllers.Customer
{
  /*The first set of methods with the T entity allow me to add any object to the database (as long as that object is defined in the database.)
   using `bool onlyActive = true` as my parameter i create a optional parameter which defaults to true when it is not passed in.
   
   ILogger is a class that allows me to output logs of different sorts to the console. This may be thins like logError or LogInformation.
   The BankContext object is used to perform database actions.
   All of my methods that retrieve information from the database are async, and hence require the await keyword with their return statement
   This return statement will wait until the action is complete before returning the object
   */
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

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public async Task<Account> GetAccountAsync(int accountId)
    {
      _logger.LogInformation($"Getting an account of {accountId}");
      // I will make the API's use token authorisation, so exposing the accountId is not a security compromise.
      IQueryable<Account> query = _context.Accounts
        .Where(a => a.AccountId == accountId);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Account[]> GetAllAccountsAsync(int customerId, bool onlyActive)
    {
      _logger.LogInformation($"Getting all accounts of {customerId}");
      IQueryable<Account> query = _context.Accounts
        .Where(a => a.CustomerId == customerId);

      if (onlyActive)
      {
        query = query.Where(a => a.Status == "active");
      }

      return await query.ToArrayAsync();
    }

    public async Task<Address> GetAddressAsync(int addressId, bool onlyActive)
    {
      _logger.LogInformation($"Getting address with id {addressId}.");

      IQueryable<Address> query = _context.Addresses
        .Where(a => a.AddressId == addressId);
      // This filers out any addresses which are not active
      if (onlyActive)
      {
        query = query.Where(a => a.Status == "active");
      }
      return await query.FirstOrDefaultAsync();

    }

    public async Task<Address[]> GetAllAddressesAsync(int customerId, bool onlyActive)
    {
      _logger.LogInformation($"Getting all address details of customer {customerId}.");
      IQueryable<Address> query = _context.Addresses
        .Where(a => a.CustomerId == customerId);
      if (onlyActive)
      {
        query = query.Where(a => a.Status == "active");
      }
      
      // Turns the results of the query into an array.
      return await query.ToArrayAsync();
    }

    public async Task<Notification> GetNotificationAsync(int notificationId, int customerId, bool onlyActive)
    {
      _logger.LogInformation($"Getting notification details of customer {customerId} with id {notificationId}.");

      IQueryable<Notification> query = _context.Notifications
        .Where(n => n.NotificationId == notificationId && n.CustomerId == customerId);

      if (onlyActive)
      {
        query = query.Where(c => c.Status == "active");
      }

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Notification[]> GetAllNotificationsAsync(int customerId, bool onlyActive)
    {
      _logger.LogInformation($"Getting notification details of customer {customerId}.");

      IQueryable<Notification> query = _context.Notifications
        .Where(n => n.CustomerId == customerId);
      if (onlyActive)
      {
        query = query.Where(n => n.Status == "active");
      }

      return await query.ToArrayAsync();
    }

    public async Task<Payee> GetPayeeAsync(int payeeId)
    {
      _logger.LogInformation($"Getting Payee with id {payeeId}.");
      IQueryable<Payee> query = _context.Payees
        .Where(p => p.PayeeId == payeeId);
      
      return await query.FirstOrDefaultAsync();
    }

    public async Task<Payee[]> GetAllPayeesAsync(int customerId)
    {
      _logger.LogInformation($"Getting all Payees for customer {customerId}");
      IQueryable<Payee> query = _context.Payees
        .Where(p => p.CustomerId == customerId);

      return await query.ToArrayAsync();
    }

    public async Task<Transaction> GetTransactionAsync(int transactionId)
    {
      _logger.LogInformation($"Getting Transaction with transactionId {transactionId}");
      IQueryable<Transaction> query = _context.Transactions
        .Where(t => t.TransactionId == transactionId);
      
      return await query.FirstOrDefaultAsync();
    }

    public async Task<Transaction[]> GetAllTransactionsAsync(string accountNumber)
    {
      _logger.LogInformation($"Getting all transactions for account {accountNumber}");
      IQueryable<Transaction> query = _context.Transactions
        .Where(t => t.AccountNumber == accountNumber);

      return await query.ToArrayAsync();
    }
  }
}
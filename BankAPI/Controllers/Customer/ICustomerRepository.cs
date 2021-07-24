using System.Threading.Tasks;
using Bank.Data.Entities;

namespace BankAPI.Controllers.Customer
{
  public interface ICustomerRepository
  {
    // This class will help me plan what actions this repository should make
    // These should be actions that I as a customer should be able to make
    
    // General
    void Add<T>(T entity) where T : class; //     Generic constraint specifying that T that is passed in
    void Delete<T>(T entity) where T : class;//   must be an entity that I've made (user-defined class 
    Task<bool> SaveChangesAsync();//              with parameter-less constructor). Adding a generic type constraint

    // Regarding Accounts
    Task<Account> GetAccountAsync(int accountId, int customerId, bool onlyActive = true);
    Task<Account[]> GetAllAccountsAsync(int customerId, bool onlyActive = true);

    // Regarding Addresses
    Task<Account> GetAddressAsync(int addressId, int customerId, bool onlyActive = true);
    Task<Account[]> GetAllAddressesAsync(int customerId, bool onlyActive = true);
    
    // Regarding Notifications
    Task<Notification> GetNotificationAsync(string status, int customerId, bool onlyActive = true);
    Task<Notification[]> GetAllNotificationsAsync(int customerId, bool onlyActive = true);
    
    // Regarding Payees
    Task<Account> GetPayeeAsync(string name, int customerId, bool onlyActive = true);
    Task<Account[]> GetAllPayeesAsync(int customerId, bool onlyActive = true);
    
    // Regarding Transactions
    Task<Transaction> GetTransactionAsync();
    Task<Transaction> GetTransactionsForAccountsAsync(Account[] accounts);

  }
}
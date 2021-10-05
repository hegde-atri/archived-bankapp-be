using System.Threading.Tasks;
using Bank.Data.Entities;

namespace Bank.API.Controllers.Customer
{
  public interface ICustomerRepository
  {
    // This class will help me plan what actions this repository should make
    // These should be actions that I as a customer should be able to make
    
    // General
    void Add<T>(T entity) where T : class; //     Generic constraint specifying that T that is passed in
    void Delete<T>(T entity) where T : class;//   must be an entity that I've made (user-defined class 
    Task<bool> SaveChangesAsync();//              with parameter-less constructor). Adding a generic type constraint

    // Update might be needed if I decide not to use Tracking in Entity Framework.
    void Update<T>(T entity) where T : class;

    // Regarding Accounts
    Task<Account> GetAccountAsync(int accountId);
    Task<Account[]> GetAllAccountsAsync(int customerId, bool onlyActive);

    // Regarding Addresses
    Task<Address> GetAddressAsync(int addressId, bool onlyActive);
    Task<Address[]> GetAllAddressesAsync(int customerId, bool onlyActive);
    
    // Regarding Notifications
    Task<Notification> GetNotificationAsync(int notificationId, int customerId, bool onlyActive);
    Task<Notification[]> GetAllNotificationsAsync(int customerId, bool onlyActive);
    
    // Regarding Payees
    Task<Payee> GetPayeeAsync(int payeeId);
    Task<Payee[]> GetAllPayeesAsync(int customerId);
    
    // Regarding Transactions
    Task<Transaction> GetTransactionAsync(int transactionId);
    Task<Transaction[]> GetAllTransactionsAsync(string accountNumber);

  }
}
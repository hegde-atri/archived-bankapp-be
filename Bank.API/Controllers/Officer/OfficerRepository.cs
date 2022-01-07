using System.Threading.Tasks;
using Bank.Data;
using Bank.Data.Entities;
using Microsoft.Extensions.Logging;

namespace Bank.API.Controllers.Officer
{
  public class OfficerRepository:IOfficerRepository
  {
        private readonly BankContext _context;
        private readonly ILogger<OfficerRepository> _logger;

        public OfficerRepository(BankContext context, ILogger<OfficerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Add<T>(T entity) where T : class
    {
      throw new System.NotImplementedException();
    }

    public void Delete<T>(T entity) where T : class
    {
      throw new System.NotImplementedException();
    }

    public void Update<T>(T entity) where T : class
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
      throw new System.NotImplementedException();
    }

    public async Task<Data.Entities.Customer> GetCustomer(CustomerVerificationModel model)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Notification[]> GetAllNotificationsAsync(int customerId)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Address[]> GetAllAddressesAsync(int customerId)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Account[]> GetAllAccountsAsync()
    {
      throw new System.NotImplementedException();
    }

    public async Task<Transaction[]> GetAllTransactionsAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}
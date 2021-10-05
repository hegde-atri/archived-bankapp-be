using System.Threading.Tasks;
using Bank.Data.Entities;

namespace Bank.API.Controllers.Teller
{
  public interface ITellerRepository
  {
    // This interface will help me plan the TellerRepository before implementing it.
    // Should be able to deposit/withdraw money and nothing else.
    // Therefore only needs the ability to create transactions and save changes.

    void Add<T>(T entity) where T : class;

    Task<bool> SaveChangesAsync();


  }
}
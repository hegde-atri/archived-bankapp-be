using System.Threading.Tasks;
using Bank.Data.Entities;

namespace BankAPI.Controllers.Teller
{
  public interface ITellerRepository
  {
    // This interface will help me plan the TellerRepository before implementing it.
    // Should be able to deposit/withdraw money and nothing else.

    void Add<T>(T entity) where T : class;

    // Debit is when you deposit money.
    Task<Transaction> DebitTransaction(string accountNo, decimal amount);
    
    //Credit is when you withdraw money.
    Task<Transaction> CreditTransaction(string accountNo, decimal amount);


  }
}
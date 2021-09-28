using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Data.Entities;
using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers.Teller
{
  [ApiController]
  [Route("api/[controller]")]
  public class TellerController: ControllerBase
  {
    private readonly ITellerRepository _repository;
    private readonly IMapper _mapper;

    // Can do two things, withdraw and deposit.
    public TellerController(ITellerRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    // This will post a debit or credit transaction note. We need only 1 transaction note since they are either
    // withdrawing and depositing with only one account.
    
    [HttpPost]
    public async Task<ActionResult<TransactionModel>> Post()
    {
      try
      {
        TransactionModel model = new TransactionModel();

        model.AccountNumber = "4353245235";
        model.Type = "credit";
        model.Amount = (decimal)(8345.54);
        model.CreatedBy = "teller: placeholder for teller name/id";
        model.Description = $"Teller transaction of type ${model.Type}";
        model.CreatedDate = DateTime.Now;
        model.TransDateTime = DateTime.Now;
        
        var transaction = _mapper.Map<Transaction>(model);
        _repository.Add(transaction);

        if (await _repository.SaveChangesAsync())
        {
          return _mapper.Map<TransactionModel>(transaction);
        }
      }
      catch(Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return (BadRequest());
    }

  }
}
using System;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Bank.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bank.API.Controllers.Customer
{
  [ApiController]
  [Route("api/customer/[controller]")]
  public class TransferController: ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public TransferController(ICustomerRepository repository, IMapper mapper,
      LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpPost]
    public async Task<ActionResult<TransferModel>> Post(TransferModel model)
    {
      try
      {
        //TODO figure out how to do these transactions that are interdependent on each other.
        // A transfer created 2 transactions that take place, but we need to make sure that both of the transactions
        // must be successful!
        TransactionModel transaction1 = new TransactionModel
        {
          Amount = model.Amount,
          AccountNumber = model.Account1AccountNumber,
          Type = model.Account1Type,
          Description = model.Account1Description,
          TransDateTime = model.Account1TransDateTime,
          CreatedBy = model.Account1CreatedBy
            
        };


        TransactionModel transaction2 = new TransactionModel
        {
          Amount = model.Amount,
          AccountNumber = model.Account2AccountNumber,
          Type = model.Account2Type,
          Description = model.Account2Description,
          TransDateTime = model.Account2TransDateTime,
          CreatedBy = model.Account2CreatedBy
        };
        
        _repository.Add(transaction1);
        _repository.Add(transaction2);

        if (await _repository.SaveChangesAsync()) return Ok();

      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }

  }
}
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

    [HttpPost]
    public async Task<ActionResult<TransactionModel>> Post(TransactionModel model)
    {
      try
      {
        var transaction = _mapper.Map<Transaction>(model);
        _repository.Add(transaction);

        if (await _repository.SaveChangesAsync())
        {
          return _mapper.Map<TransactionModel>(transaction);
        }
        else
        {
          return BadRequest();
        }
        
      }
      catch(Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

  }
}
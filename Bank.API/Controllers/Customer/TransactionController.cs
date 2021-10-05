using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.API.Models;
using Bank.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bank.API.Controllers.Customer
{
  //  TODO figure out how a customer transaction would translate to an API being called.
  [ApiController]
  [Route("api/customer/[controller]")]
  public class TransactionController : ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public TransactionController(ICustomerRepository repository, IMapper mapper,
      LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpGet("{transactionId}")]
    public async Task<ActionResult<TransactionModel>> Get(int transactionId)
    {
      try
      {
        var result = await _repository.GetTransactionAsync(transactionId);
        if (result == null) return BadRequest();

        return _mapper.Map<TransactionModel>(result);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpGet("all")]
    public async Task<ActionResult<TransactionModel[]>> Get(string accountNumber)
    {
      try
      {
        var results = await _repository.GetAllTransactionsAsync(accountNumber);

        return _mapper.Map<TransactionModel[]>(results);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }



  }
}
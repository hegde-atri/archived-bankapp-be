using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Data.Entities;
using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace BankAPI.Controllers.Customer
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly CustomerRepository _repository;
    private readonly ILogger<CustomerController> _logger;
    private readonly LinkGenerator _linkGenerator;
    private readonly BankContext _context;

    public CustomerController(CustomerRepository repository, ILogger<CustomerController> logger, LinkGenerator linkGenerator, BankContext context)
    {
      _repository = repository;
      _logger = logger;
      _linkGenerator = linkGenerator;
      _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<Payee>> AddPayee(PayeeModel x)
    {
      // try
      // {
      //   var existing = await _repository.GetPayeeAsync();
      // }
      // catch (Exception e)
      // {
      //   _logger.LogError(e.ToString());
      // }
      return Ok();
    }
    
    // Gets all Payees
    [HttpGet]
    public async Task<ActionResult<Payee[]>> GetPayee()
    {
      return Ok();
    }
    
  }
}
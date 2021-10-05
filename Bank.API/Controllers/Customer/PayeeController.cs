using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.API.Models;
using Bank.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// This class will be used to retrieve/add/modify Payees as a customer

namespace Bank.API.Controllers.Customer
{
  [ApiController]
  [Route("api/customer/[controller]")]
  public class PayeeController : ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public PayeeController(ICustomerRepository repository, IMapper mapper, LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpGet("{payeeId}")]
    public async Task<ActionResult<PayeeModel>> Get(int payeeId)
    {
      try
      {
        var result = await _repository.GetPayeeAsync(payeeId);
        if (result == null) return BadRequest();
        return _mapper.Map<PayeeModel>(result);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpGet("all")]
    public async Task<ActionResult<PayeeModel[]>> Get()
    {
      // TODO customer ID 
      var customerId = 1;
      try
      {
        var results = await _repository.GetAllPayeesAsync(customerId);

        return _mapper.Map<PayeeModel[]>(results);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpPut("payeeId")]
    public async Task<ActionResult<PayeeModel>> Put(int payeeId, PayeeModel model)
    {
      try
      {
        var old = await _repository.GetPayeeAsync(payeeId);
        if (old == null) return BadRequest("Payee not found");

        _mapper.Map(model, old); // maps values from "model" to "old"
        old.PayeeId = payeeId;
        if (await _repository.SaveChangesAsync()) return _mapper.Map<PayeeModel>(old);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }

    [HttpPost]
    public async Task<ActionResult<PayeeModel>> Post(PayeeModel model)
    {
      try
      {
        var location = _linkGenerator.GetPathByAction("Get", "Payee",
          new {model.PayeeId});

        if (string.IsNullOrWhiteSpace(location)) return BadRequest();
        var payee = _mapper.Map<Payee>(model);
        _repository.Add(payee);
        if (await _repository.SaveChangesAsync())
        {
          return Created(location, _mapper.Map<PayeeModel>(payee));
        }
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }
    
    [HttpDelete]
    public async Task<ActionResult<PayeeModel>> Delete(int payeeId)
    {
      try
      {
        var old = await _repository.GetPayeeAsync(payeeId);
        if (old == null) return BadRequest("Payee not found");
        
        _repository.Delete(old);
        if (await _repository.SaveChangesAsync()) return Ok("Deleted payee successfully");
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }
  }
}
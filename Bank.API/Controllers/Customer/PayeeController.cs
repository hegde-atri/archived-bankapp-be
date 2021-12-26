using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.API.Models;
using Bank.Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// This class will be used to retrieve/add/modify Payees as a customer

namespace Bank.API.Controllers.Customer
{
  [EnableCors("_myAllowSpecificOrigins")]
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

    [HttpGet]
    public async Task<ActionResult<PayeeModel[]>> Get(CustomerRequest model)
    {
      try
      {
        if (model.CustomerId != 0)
        {
          var results = await _repository.GetAllPayeesAsync(model.CustomerId);
          return _mapper.Map<PayeeModel[]>(results);
        }
        else if (model.PayeeId[0] != 0)
        {
          // You can get only the first address back.
          var result = new Payee[]{await _repository.GetPayeeAsync(model.PayeeId[0])};
          if (result == null) return BadRequest();
          return _mapper.Map<PayeeModel[]>(result);
        }
        
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
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
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
  // This will be used to handle all the actions related to customer address
  // This class responds to API calls related to addresses.
  
  [ApiController]
  [Route("api/customer/[controller]")]
  public class AddressController: ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public AddressController(ICustomerRepository repository, IMapper mapper, 
      LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpPost]
    public async Task<ActionResult<AddressModel>> Post(AddressModel model)
    {
      try
      {
        var location = _linkGenerator.GetPathByAction("Get", "Address", 
          new {model.AddressId});
        
        if (string.IsNullOrWhiteSpace(location)) return BadRequest();
        var address = _mapper.Map<Address>(model);
        _repository.Add(address);
        if (await _repository.SaveChangesAsync())
        {
          return Created(location, _mapper.Map<AddressModel>(address));
        }

      }
      catch (Exception e)
      {
        // _logger.LogWarning("Failed to port address with error: " + e);
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }

    [HttpGet("{addressId}")]
    public async Task<ActionResult<AddressModel>> Get(int addressId, bool onlyActive = true)
    {
      try
      {
        var result = await _repository.GetAddressAsync(addressId);
        if (result == null) return BadRequest();
        return _mapper.Map<AddressModel>(result);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpGet("all")]
    public async Task<ActionResult<AddressModel[]>> Get(bool onlyActive = true)
    {
      // TODO customerId must be taken from authorisation API
      int customerId = 1;
      try
      {
        var results = await _repository.GetAllAddressesAsync(customerId);
        
        return _mapper.Map<AddressModel[]>(results);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpPut("{addressId}")]
    public async Task<ActionResult<AddressModel>> Put(int addressId, AddressModel model, bool onlyActive)
    {
      try
      {
        var old = await _repository.GetAddressAsync(addressId);
        if (old == null) return BadRequest("Address not found");

        _mapper.Map(model, old);
        old.AddressId = addressId;
        if (await _repository.SaveChangesAsync()) return _mapper.Map<AddressModel>(old);

      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }
  }
}
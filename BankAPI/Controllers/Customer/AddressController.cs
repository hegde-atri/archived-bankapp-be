﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Data.Entities;
using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAPI.Controllers.Customer
{
  
  [Route("api/[controller]")]
  [ApiController]
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
        else
        {
          return BadRequest();
        }

      }
      catch (Exception e)
      {
        // _logger.LogWarning("Failed to port address with error: " + e);
        return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
      }
    }

    [HttpGet("{addressId}")]
    public async Task<ActionResult<AddressModel>> Get(int addressId)
    {
      try
      {
        var result = await _repository.GetAddressAsync(addressId);
        if (result == null) return BadRequest();
        return _mapper.Map<AddressModel>(result);
      }
      catch (Exception e)
      {
        // _logger.LogWarning("Failed to get address with error: " + e);
        return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
      }
    }

    [HttpPut("{addressId}")]
    public async Task<ActionResult<AddressModel>> Put(int addressId, AddressModel model)
    {
      try
      {
        var old = await _repository.GetAddressAsync(addressId, false);
        if (old == null) return BadRequest("old address not found");

        _mapper.Map(model, old);
        // _repository.Update(old);
        if (await _repository.SaveChangesAsync()) return _mapper.Map<AddressModel>(old);

      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
      }

      return BadRequest("Give up and move on");
    }
  }
}
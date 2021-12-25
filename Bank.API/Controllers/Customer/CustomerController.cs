﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Bank.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bank.API.Controllers.Customer
{
  [ApiController]
  [Route("api/customer/[controller]")]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public CustomerController(ICustomerRepository repository, IMapper mapper, LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<CustomerModel>> Get(int customerId)
    {
      try
      {
        var result = await _repository.GetCustomerByIdAsync(customerId);
        if (result == null) return BadRequest();
        return _mapper.Map<CustomerModel>(result);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
      
    }



  }

}
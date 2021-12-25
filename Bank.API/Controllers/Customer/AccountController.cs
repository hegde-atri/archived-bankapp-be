using System;
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
  public class AccountController: ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public AccountController(ICustomerRepository repository, IMapper mapper, 
      LinkGenerator linkGenerator)
    {
      // object instances we will be using in this class
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    // [HttpGet]
    // public async Task<ActionResult<AccountModel>> Get(CustomerRequest model)
    // {
    //   try
    //   {
    //     if (model.CustomerId != null)
    //     {
    //     }
    //     else if (model.AccountId != null)
    //     {
    //       
    //     }
    //   }
    //   catch (Exception e)
    //   {
    //     StatusCode(StatusCodes.Status500InternalServerError, e);
    //   }
    // }


  }
}
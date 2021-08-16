using AutoMapper;
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
    
    
  }
}
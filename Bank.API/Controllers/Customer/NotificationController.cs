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
  [ApiController]
  [Route("api/customer/[controller]")]
  public class NotificationController: ControllerBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public NotificationController(ICustomerRepository repository, IMapper mapper, LinkGenerator linkGenerator)
    {
      _repository = repository;
      _mapper = mapper;
      _linkGenerator = linkGenerator;
    }

    [HttpGet("{notificationId}")]
    public async Task<ActionResult<NotificationModel>> Get(int notificationId)
    {
      int customerId = 1;
      try
      {
        var result = await _repository.GetNotificationAsync(notificationId, customerId);
        if (result == null) return BadRequest();
        return _mapper.Map<NotificationModel>(result);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    [HttpGet]
    public async Task<ActionResult<NotificationModel[]>> Get(bool onlyActive)
    {
      int customerId = 1;
      try
      {
        var results = await _repository.GetAllNotificationsAsync(customerId);
        return _mapper.Map<NotificationModel[]>(results);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }


    [HttpPost]
    public async Task<ActionResult<NotificationModel>> Post(NotificationModel model)
    {
      try
      {
        var location = _linkGenerator.GetPathByAction("Get", "Notification",
          new {model.NotificationId});
        if (string.IsNullOrWhiteSpace(location)) return BadRequest();
        var notification = _mapper.Map<Notification>(model);
        _repository.Add(notification);

        if (await _repository.SaveChangesAsync())
        {
          return Created(location, _mapper.Map<NotificationModel>(notification));
        }
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }

      return BadRequest();
    }


  }
}
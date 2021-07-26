using AutoMapper;
using Bank.Data.Entities;
using BankAPI.Models;

namespace BankAPI.Controllers.Customer
{
  public class CustomerProfile:Profile
  {
    public CustomerProfile()
    {
      this.CreateMap<Address, AddressModel>()
        .ReverseMap()
        .ForMember(a => a.AddressId, opt => opt.Ignore())
        .ForMember(a => a.CustomerId, opt => opt.Ignore());

      this.CreateMap<Transaction, TransactionModel>().ReverseMap();
      this.CreateMap<Notification, NotificationModel>().ReverseMap();
      this.CreateMap<Payee, PayeeModel>().ReverseMap();
    }
  }
}
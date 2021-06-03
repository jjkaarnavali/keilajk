using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PurchaseReceivedPageService : BaseService, IPurchaseReceivedPageService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IMapper Mapper;
        public PurchaseReceivedPageService(IAppUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            Mapper = mapper;
        }
        
        public async Task CreatePayment(Guid id, Guid userId)
        {
            // Create payment
            var bills = await _uow.Bills.GetAllAsync(userId);
            var orders = await _uow.Orders.GetAllAsync(userId);
            BLL.App.DTO.Order orderToPay = new BLL.App.DTO.Order();
            BLL.App.DTO.Bill billToPay = new BLL.App.DTO.Bill();;
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Until == null)
                {
                    orderToPay = Mapper.Map(order, new BLL.App.DTO.Order());
                }
            }

            foreach (var bill in bills)
            {
                if (bill.OrderId == orderToPay.Id)
                {
                    billToPay = Mapper.Map(bill, new BLL.App.DTO.Bill());
                }
            }
            BLL.App.DTO.Payment payment = new BLL.App.DTO.Payment();
            payment.Id = Guid.NewGuid();
            payment.PaymentTypeId = id;
            payment.BillId = billToPay.Id;
            payment.PersonId = billToPay.PersonId;
            payment.PaymentTime = DateTime.Now;
            _uow.Payments.Add(Mapper.Map(payment, new DAL.App.DTO.Payment()));
            await _uow.SaveChangesAsync();
            
            // Mark orders  as completed
            orderToPay.Until = DateTime.Now;
            _uow.Orders.Update(Mapper.Map(orderToPay, new DAL.App.DTO.Order()));
            await _uow.SaveChangesAsync();
        }
        
        
        
    }
}
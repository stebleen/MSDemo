using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.Models.ViewModel;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        // 还依赖于购物车服务和地址簿服务
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAddressBookService _addressBookService;

        public OrderService(IUnitOfWork<MSDbContext> unitOfWork,
                        IShoppingCartService shoppingCartService,
                        IAddressBookService addressBookService,
                        IMapper mapper, IdWorker idWorker)
        : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartService = shoppingCartService;
            _addressBookService = addressBookService;
        }

        public async Task<OrderResponseDto> SubmitOrderAsync(Orders submitOrderDto, long userId)
        {
            
            // 检查用户的地址簿 addressBookId 是否存在
            var addressBook = await _addressBookService.GetAddressBookByIdAsync(submitOrderDto.AddressBookId);
            if (addressBook == null)
            {
                throw new Exception("无效的地址簿ID");
            }
            

            
            // 检查购物车是否为空
            var cartItems = await _shoppingCartService.GetShoppingCart(userId);
            if (cartItems == null || !cartItems.Any())  // // 使用 .Any() 方法检查集合是否为空
            {
                throw new Exception("购物车为空");
            }
            

            // 创建订单实例
            var newOrder = new Orders
            {
                UserId = userId,
                AddressBookId = submitOrderDto.AddressBookId,
                Amount = submitOrderDto.Amount,
                DeliveryStatus = submitOrderDto.DeliveryStatus,
                // EstimatedDeliveryTime = submitOrderDto.EstimatedDeliveryTime,
                OrderTime = DateTime.Now,
                PayMethod = submitOrderDto.PayMethod,
                PayStatus = 1, // 1为已支付状态
                Remark = submitOrderDto.Remark,
                Status = 2, // 2为待接单状态
                Number = GenerateOrderNumber(), // 生成订单号的方法
                PackAmount = submitOrderDto.PackAmount,
                TablewareNumber = submitOrderDto.TablewareNumber,
                TablewareStatus = submitOrderDto.TablewareStatus
            };

            _unitOfWork.GetRepository<Orders>().Insert(newOrder);

            // 处理订单明细信息
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.Id,
                    // ...其他字段从item中赋值
                };

                _unitOfWork.GetRepository<OrderDetail>().Insert(orderDetail);
            }

            // 保存所有更改
            await _unitOfWork.SaveChangesAsync();

            // 清空用户购物车
            await _shoppingCartService.CleanCartAsync(userId);

            // 返回订单响应DTO
            return new OrderResponseDto
            {
                Id = newOrder.Id,
                OrderAmount = newOrder.Amount,
                OrderNumber = newOrder.Number,
                OrderTime = newOrder.OrderTime
            };
        }

        private string GenerateOrderNumber()
        {
            // 生成唯一的订单号逻辑
            return DateTime.Now.Ticks.ToString();
        }
    }

}


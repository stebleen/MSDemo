using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.admin;
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
        private readonly IUserService _userService;

        public OrderService(IUnitOfWork<MSDbContext> unitOfWork,
                        IShoppingCartService shoppingCartService,
                        IAddressBookService addressBookService,
                        IUserService userService,
                        IMapper mapper, IdWorker idWorker)
        : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartService = shoppingCartService;
            _addressBookService = addressBookService;
            _userService = userService;
        }

        public async Task<OrderResponseDto> SubmitOrderAsync(Orders submitOrderDto, long userId)
        {
            // 获取用户信息以获取电话号码
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }


            // 检查用户的地址簿 addressBookId 是否存在
            var addressBook = await _addressBookService.GetAddressBookByIdAsync(submitOrderDto.AddressBookId);
            if (addressBook == null)
            {
                throw new Exception("无效的地址簿ID");
            }


            // 检查地址簿中的地址 AddressId 是否有效，并获取地址
            var address = await _unitOfWork.GetRepository<Address>().GetFirstOrDefaultAsync(
                predicate: a => a.Id == addressBook.AddressId,
                disableTracking: false
            );
            if (address == null)
            {
                throw new Exception("无效的地址ID");
            }
            // 拼接地址中的campusName和buildingName
            string fullAddress = $"{address.CampusName}{address.BuildingName}";



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
                TablewareStatus = submitOrderDto.TablewareStatus,
                Address = fullAddress,  // 使用拼接后的完整地址
                Phone = user.Phone,
                UserName = user.Name,
                Consignee = user.Name,
            };

            _unitOfWork.GetRepository<Orders>().Insert(newOrder);
            // 要先保存！！
            await _unitOfWork.SaveChangesAsync();

            // 处理订单明细信息
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.Id,
                    // 根据 ShoppingCart 中是 Dish 还是 Setmeal 设置相应ID
                    DishId = cartItem.DishId ?? 0,
                    SetmealId = cartItem.SetmealId ?? 0,
                    Name = cartItem.Name,
                    Image = cartItem.Image,
                    Amount = cartItem.Amount,
                    Number = cartItem.Number,
                    DishFlavor = cartItem.DishFlavor,
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


        public async Task<PaymentInfoDto> PayOrderAsync(string orderNumber, int payMethod)
        {
            // 示例：查找订单
            var orderRepository = _unitOfWork.GetRepository<Orders>();

            // 使用 GetFirstOrDefaultAsync 方法根据订单号查询订单
            var order = await orderRepository.GetFirstOrDefaultAsync(
                predicate: o => o.Number == orderNumber,
                disableTracking: false
            );

            if (order == null)
            {
                throw new Exception("订单不存在");
            }

            // 示例：支付处理逻辑
            // 根据payMethod调用不同支付服务，然后处理支付结果

            // 示例：返回支付信息模拟
            return new PaymentInfoDto
            {
                NonceStr = GenerateNonceStr(),
                PaySign = "模拟签名",
                TimeStamp = GenerateTimeStamp(),
                SignType = "MD5", // 仅示例，实际根据支付服务提供商要求
                PackageStr = "prepay_id=示例"
            };
        }

        private string GenerateNonceStr()
        {
            // 生成随机字符串
            return Guid.NewGuid().ToString("N");
        }

        private string GenerateTimeStamp()
        {
            // 生成时间戳
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }


        public async Task<OrderSearchResponseDto> SearchOrdersAsync(OrderSearchRequestDto requestDto)
        {
            var orderQuery = _unitOfWork.GetRepository<Orders>().GetAll();


            // 条件筛选
            if (!string.IsNullOrEmpty(requestDto.Phone))
            {
                orderQuery = orderQuery.Where(o => o.Phone.Contains(requestDto.Phone));
            }
            if (!string.IsNullOrEmpty(requestDto.Number))
            {
                orderQuery = orderQuery.Where(o => o.Number == requestDto.Number);
            }
            if (!string.IsNullOrEmpty(requestDto.Status))
            {
                if (int.TryParse(requestDto.Status, out int status))
                {
                    orderQuery = orderQuery.Where(o => o.Status == status);
                }
            }
            if (DateTime.TryParse(requestDto.BeginTime, out DateTime beginTime))
            {
                orderQuery = orderQuery.Where(o => o.OrderTime >= beginTime);
            }
            if (DateTime.TryParse(requestDto.EndTime, out DateTime endTime))
            {
                orderQuery = orderQuery.Where(o => o.OrderTime <= endTime);
            }


            int total = await orderQuery.CountAsync();
            //int total = 40;

            var orders = await orderQuery
                .OrderBy(o => o.Id)
                .Skip((requestDto.Page - 1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToListAsync();

            

            /*
            // 一次性获取所有查询订单的详情
            var orderIds = orders.Select(o => o.Id).ToList();
            
            var orderDetails = await _unitOfWork.GetRepository<OrderDetail>()
                .GetAll()
                .Where(d => orderIds.Contains(d.OrderId))
                .ToListAsync();
            */
            

            var orderDetails = _unitOfWork.GetRepository<OrderDetail>().GetAll().ToList();

            var records = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Number = o.Number,
                Status = o.Status,
                UserId = o.UserId,
           
                UserName = o.UserName,
                Consignee = o.Consignee,
                AddressBookId = o.AddressBookId,
                //OrderTime = o.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //CheckoutTime=o.CheckoutTime.ToString("yyyy-MM-dd HH:mm:ss"),
                PackAmount = o.PackAmount,
                PayMethod = o.PayMethod,
                Amount = o.Amount,
                Remark = o.Remark,
                Phone = o.Phone,
                Address = o.Address,
                CancelReason = o.CancelReason,
                RejectionReason = o.RejectionReason,
                //CancelTime=o.CancelTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //EstimatedDeliveryTime= o.EstimatedDeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"),
                DeliveryStatus = o.DeliveryStatus,
                //DeliveryTime = o.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"),
                TablewareNumber = o.TablewareNumber,
                TablewareStatus = o.TablewareStatus,

                OrderTime = o.OrderTime.HasValue ? o.OrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                CheckoutTime = o.CheckoutTime.HasValue ? o.CheckoutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                CancelTime = o.CancelTime.HasValue ? o.CancelTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                EstimatedDeliveryTime = o.EstimatedDeliveryTime.HasValue ? o.EstimatedDeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                DeliveryTime = o.DeliveryTime.HasValue ? o.DeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",

                OrderDishes = String.Join("; ", orderDetails
                .Where(d => d.OrderId == o.Id)
                .Select(d => $"{d.Name}*{d.Number}")),
            }).ToList();

            return new OrderSearchResponseDto
            {
                Total = total,
                Records = records
            };
        }

        public async Task<OrderStatisticsVO> GetOrderStatisticsAsync()
        {
            var orders = _unitOfWork.GetRepository<Orders>().GetAll();

            var statistics = new OrderStatisticsVO
            {
                ToBeConfirmed = await orders.CountAsync(o => o.Status == 2),
                Confirmed = await orders.CountAsync(o => o.Status == 3),
                DeliveryInProgress = await orders.CountAsync(o => o.Status == 4),
            };

            return statistics;
        }


        public async Task<OrderDetailsResponse> GetOrderDetailsAsync(long orderId)
        {
            var order = await _unitOfWork.GetRepository<Orders>().GetFirstOrDefaultAsync(
                predicate: o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var orderDetailsList = await _unitOfWork.GetRepository<OrderDetail>()
                .GetAll()
                .Where(d => d.OrderId == orderId)
                .ToListAsync();

            var response = new OrderDetailsResponse
            {
                Id = order.Id,
                Number = order.Number,
                Status = order.Status,
                UserId = order.UserId,
                AddressBookId = order.AddressBookId,
                PayMethod = order.PayMethod,
                PayStatus = order.PayStatus,
                Amount = order.Amount,
                Remark = order.Remark,
                Phone = order.Phone,
                Address = order.Address,
                UserName = order.UserName,
                Consignee = order.Consignee,
                CancelReason = order.CancelReason,
                RejectionReason = order.RejectionReason,
                DeliveryStatus = order.DeliveryStatus,
                PackAmount = order.PackAmount,
                TablewareNumber = order.TablewareNumber,
                TablewareStatus = order.TablewareStatus,
                //OrderTime = order.OrderTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "未知",
                //CheckoutTime = order.CheckoutTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "未知",
                OrderTime = order.OrderTime.HasValue ? order.OrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                CheckoutTime = order.CheckoutTime.HasValue ? order.CheckoutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                CancelTime = order.CancelTime.HasValue ? order.CancelTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                EstimatedDeliveryTime = order.EstimatedDeliveryTime.HasValue ? order.EstimatedDeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                DeliveryTime = order.DeliveryTime.HasValue ? order.DeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",

                OrderDetailList = orderDetailsList.Select(od => new OrderDetailVO
                {
                    Amount = od.Amount,
                    DishFlavor = od.DishFlavor,
                    DishId = od.DishId,
                    Id = od.Id,
                    Image = od.Image,
                    Name = od.Name,
                    Number = od.Number,
                    OrderId = od.OrderId,
                    SetmealId = od.SetmealId
                }).ToList(),

                OrderDishes = null
            };

            return response;
        }


    }

}


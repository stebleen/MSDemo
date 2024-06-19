using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using AutoMapper;
using MS.Common.IDCode;

namespace MS.Services
{
    public class BusinessDataService : BaseService, IBusinessDataService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        public BusinessDataService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }



        [DllImport("BusinessStatsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double CalculateOrderCompletionRate(int validOrdersCount, int totalOrdersCount);

        public async Task<BusinessDataVO> GetTodayBusinessDataAsync()
        {

            /**
             * 营业额：当日已完成订单的总金额
             * 有效订单：当日已完成订单的数量
             * 订单完成率：有效订单数 / 总订单数
             * 平均客单价：营业额 / 有效订单数
             * 新增用户：当日新增用户的数量
             */

            var today = DateTime.Today;
            var newUsers = await _unitOfWork.GetRepository<User>().GetAllAsync(u => u.CreateTime.Date == today);
            var totalOrders = await _unitOfWork.GetRepository<Orders>().GetAllAsync(o => o.OrderTime.Value.Date == today);
            var validOrders = totalOrders.Where(o => o.Status == 5);
            var turnover = validOrders.Sum(o => o.Amount);
            var validOrderCount = validOrders.Count();
            var totalOrderCount = totalOrders.Count();

            var orderCompletionRate = 0.0;

            try
            {
                orderCompletionRate = CalculateOrderCompletionRate(validOrderCount, totalOrderCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (totalOrderCount == 0)
                {
                    orderCompletionRate = 0.0;
                }
                else
                    orderCompletionRate = validOrderCount / totalOrderCount;
            }
            //var orderCompletionRate = CalculateOrderCompletionRate(validOrderCount, totalOrderCount);
            var unitPrice = validOrderCount > 0 ? turnover / validOrderCount : 0;

            return new BusinessDataVO
            {
                NewUsers = newUsers.Count(),
                OrderCompletionRate = orderCompletionRate,
                Turnover = turnover,
                UnitPrice = unitPrice,
                ValidOrderCount = validOrderCount,
                //TotalOrderCount = totalOrderCount
            };
        }
    }
}

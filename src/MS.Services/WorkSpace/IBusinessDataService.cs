using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{

    public interface IBusinessDataService : IBaseService
    {
        Task<BusinessDataVO> GetTodayBusinessDataAsync();
    }

    public class BusinessDataVO
    {
        public int NewUsers { get; set; }
        public double OrderCompletionRate { get; set; }
        public decimal Turnover { get; set; }
        public decimal UnitPrice { get; set; }
        public int ValidOrderCount { get; set; }
        //public int TotalOrderCount { get; set; }
    }
    
}

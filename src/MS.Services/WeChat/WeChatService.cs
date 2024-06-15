using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class WeChatService : BaseService, IWeChatService
    {
        public WeChatService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker)
            : base(unitOfWork, mapper, idWorker)
        {
        }

        public async Task<WeChatResult> GetOpenIdAndSessionKeyAsync(string appId, string secret, string code)
        {
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={secret}&js_code={code}&grant_type=authorization_code";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<WeChatResult>(response);
            }
        }
    }
}

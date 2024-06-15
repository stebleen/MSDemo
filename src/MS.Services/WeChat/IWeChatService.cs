using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IWeChatService : IBaseService
    {
        Task<WeChatResult> GetOpenIdAndSessionKeyAsync(string appId, string secret, string code);
    }
}

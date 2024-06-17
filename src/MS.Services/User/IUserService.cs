using MS.Entities;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IUserService : IBaseService
    {
        Task<ExecuteResult<User>> HandleWeChatLoginAsync(string appId, string secret, string code);

        Task<User> GetUserByIdAsync(long userId);
    }
}

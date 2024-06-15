using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MS.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IWeChatService _weChatService;

        public UserService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, IWeChatService weChatService) : base(unitOfWork, mapper, idWorker)
        {
            _weChatService = weChatService;
        }

        public async Task<ExecuteResult<User>> HandleWeChatLoginAsync(string appId, string secret, string code)
        {
            var result = new ExecuteResult<User>();

            var weChatResult = await _weChatService.GetOpenIdAndSessionKeyAsync(appId, secret, code);
            if (weChatResult.Openid == null)
            {
                // weChatResult.Openid = "oPDQK7XGdTa74xJj7QLbfPMYJ5fI";
                return result.SetFailMessage("微信登录失败");
            }

            var user = _unitOfWork.GetRepository<User>().GetAll().FirstOrDefault(u => u.OpenId == weChatResult.Openid);
            if (user == null)
            {
                // 如果用户不存在，则创建一个新用户
                user = new User { OpenId = weChatResult.Openid };
                _unitOfWork.GetRepository<User>().Insert(user);
                await _unitOfWork.SaveChangesAsync();
            }

            return result.SetData(user);
        }
    }
}

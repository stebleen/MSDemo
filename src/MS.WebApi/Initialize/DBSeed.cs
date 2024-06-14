using MS.Common.Security;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.Core;
using MS.UnitOfWork;
using System;

namespace MS.WebApi
{
    public static class DBSeed
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns>返回是否创建了数据库（非迁移）</returns>
        public static bool Initialize(IUnitOfWork<MSDbContext> unitOfWork)
        {
            /*
            bool isCreateDb = false;
            //直接自动执行迁移,如果它创建了数据库，则返回true
            if (unitOfWork.DbContext.Database.EnsureCreated())
            {
                isCreateDb = true;
                //打印log-创建数据库及初始化期初数据

                long rootTUserId = 12194900;

                #region 角色、用户、登录
                Role rootRole = new Role
                {
                    Id = 12194900,
                    Name = "SuperAdmin",
                    DisplayName = "超级管理员",
                    Remark = "系统内置超级管理员",
                    Creator = rootTUserId,
                    CreateTime = DateTime.Now,
                    StatusCode = StatusCode.Enable,
                };
                TUser rootTUser = new TUser
                {
                    Id = rootTUserId,
                    Account = "admin",
                    Name = "admin",
                    RoleId = rootRole.Id,
                    StatusCode = StatusCode.Enable,
                    Creator = rootTUserId,
                    CreateTime = DateTime.Now,
                };

                unitOfWork.GetRepository<Role>().Insert(rootRole);
                unitOfWork.GetRepository<TUser>().Insert(rootTUser);
                unitOfWork.GetRepository<UserLogin>().Insert(new UserLogin
                {
                    UserId = rootTUserId,
                    Account = rootTUser.Account,
                    HashedPassword = Crypto.HashPassword(rootTUser.Account),//默认密码同账号名
                    IsLocked = false
                });
                unitOfWork.SaveChanges();

                #endregion
            }

            return isCreateDb;
            */
            return true;
        }


    }
}

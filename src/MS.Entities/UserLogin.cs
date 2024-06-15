using MS.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace MS.Entities
{
    public class UserLogin : IEntity
    {
        /*
        public long UserId { get; set; }
        public string Account { get; set; }
        public string HashedPassword { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? AccessFailedCount { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockedTime { get; set; }

        public TUser User { get; set; }
        */

        public long Id { get; set; }

        public string OpenId { get; set; }
        public UserLogin userLogin { get; set; }

    }
}

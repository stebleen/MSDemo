using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class LoginResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}

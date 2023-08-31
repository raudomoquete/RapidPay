using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthSysPay.Core
{
    public class AuthRequest
    {
        public AuthRequest()
        {
            this.UserName = String.Empty;
            this.Password = String.Empty;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}

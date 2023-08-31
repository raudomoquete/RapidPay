using AuthSysPay.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthSysPay.Infrastructure
{
    public class AuthSysPayContext : IdentityDbContext<User>
    {
        //public AuthSysPayContext()
        //{
        //}

        public AuthSysPayContext(DbContextOptions<AuthSysPayContext> options) : base(options)
        {        
        }

        //public virtual DbSet<User> Users { get; set; }

    }
}

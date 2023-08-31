//using AuthSysPay.Core;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace AuthSysPay.Infrastructure
//{
//    public static class ServiceCollectionExtension
//    {
//        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
//        {
//            services.AddDbContext<AuthSysPayContext>(options =>
//            options.UseSqlServer(config.GetConnectionString("AuthSysPayConnection")));

//            return services;
//        }
//    }
//}

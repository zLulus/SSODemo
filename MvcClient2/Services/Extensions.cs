using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    /// <summary>
    /// 拓展方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 为了保留IConfiguration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddUrlResolve(this IServiceCollection services, IConfiguration config)
        {
            services.Add(new ServiceDescriptor(typeof(UrlResolveService), typeof(UrlResolveService), ServiceLifetime.Singleton));
            //读取配置文件
            UrlResolveService.ApiUrl = config["ApiUrl"];
            UrlResolveService.AuthorityUrl = config["AuthorityUrl"];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IdentityServerApi.Dtos;

namespace IdentityServerApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // configure identity server with in-memory stores, keys, clients and scopes
            //配置IdentityServer服务
            services
                //在DI系统里面添加IdentityServer,为运行时状态注册内存存储
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                //范围定义了您系统中的API资源列表
                .AddInMemoryApiResources(Config.GetApiResources())
                //客户Client列表
                .AddInMemoryClients(Config.GetClients());

            //AutoMapper
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseIdentityServer();
            
        }
    }
}

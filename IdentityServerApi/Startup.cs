using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IdentityServerApi.EntityFramework;

namespace IdentityServerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            //todo
            //https://aspnetboilerplate.com/Pages/Documents/Zero/Identity-Server
            //https://www.codeproject.com/Articles/1115763/Using-ASP-NET-Core-Entity-Framework-Core-and-ASP-N
            //https://aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration
            //https://aspnetboilerplate.com/Pages/Documents
            //添加ef的依赖  
            //string c = Configuration.GetConnectionString("SSODemoConnection");
            //var connection = "server=115.28.102.108;uid=sa;pwd=Woshizenglu9501;database=SSODemoDb";
            //services.AddDbContext<SSOContext>(options => options.UseSqlServer(connection));
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

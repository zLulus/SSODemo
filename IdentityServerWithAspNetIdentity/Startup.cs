﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Services;
using AutoMapper;
using IdentityServer4.Validation;
using IdentityServer4.Services;

namespace IdentityServerWithAspNetIdentity
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ConfigurationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //.AddEntityFrameworkStores<ConfigurationDbContext>()
                .AddDefaultTokenProviders();

            //aspuser password policy  修改密码的相关规则
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                //是否要求有数字
                options.Password.RequireDigit = false;
                //密码要求的最小长度
                options.Password.RequiredLength = 6;
                //是否要求有非字母数字的字符
                options.Password.RequireNonAlphanumeric = false;
                //是否要求有大写的ASCII字母
                options.Password.RequireUppercase = false;
                //是否要求有小写的ASCII字母
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
            });


            // Add application services.
            //注入服务
            services.AddTransient<IEmailSender, EmailSender>();
            //短信服务
            services.AddTransient<IMessageSender, MessageSender>();
            services.AddTransient<ISendMessageLogService, SendMessageLogService>();
            //config
            services.AddUrlResolve(Configuration);
            //client服务
            //services.AddTransient<IIdentityServer4ClientService, IdentityServer4ClientService>();

            services.AddMvc();

            //AutoMapper
            services.AddAutoMapper();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "998042782978-s07498t8i8jas7npj4crve1skpromf37.apps.googleusercontent.com";
                    options.ClientSecret = "HsnwJri_53zn7VcO1Fm7THBb";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // app.UseIdentity(); // not needed, since UseIdentityServer adds the authentication middleware
            //注意UseIdentityServer放在路由设置前面
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

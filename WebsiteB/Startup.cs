// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                //将身份验证服务添加到DI和配置“Bearer”作为默认模式
                .AddAuthorization()
                .AddJsonFormatters();

            //将标识服务器访问令牌验证处理程序添加到DI，供身份验证服务使用
            //配置identityServer授权
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "api1";
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            //将身份验证中间件添加到管道中，以便在每次对主机的调用中自动执行身份验证
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
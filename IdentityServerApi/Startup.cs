// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServerWithAspNetIdentity.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace IdentityServerWithAspNetIdentity
{
    public class Config
    {
        // scopes define the resources in your system
        //资源
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email()
            };
        }

        //api
        public static IEnumerable<ApiResource> GetApiResources()
        {
            //这个是api的配置位置，目前只有一个
            return new List<ApiResource>
            {
                new ApiResource("jwellApi", "积微循环API接口")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration Configuration)
        {
            var urls = Configuration["ClientUrls"].Split(';');
            ICollection<string> redirectUris = new List<string>();
            ICollection<string> postLogoutRedirectUris = new List<string>();
            foreach (var url in urls)
            {
                redirectUris.Add($"{url}/signin-oidc");
                postLogoutRedirectUris.Add($"{url}/signout-callback-oidc");
            }
            //只用一个client,所有客户端共用
            var clients = new List<Client>
            {
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    //ClientId不能重复
                    ClientId = "mvc",
                    ClientName = "积微物联",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    //这里写入配置文件,以分好隔开,几个客户端就用几个网址
                    RedirectUris =redirectUris,
                    PostLogoutRedirectUris =postLogoutRedirectUris,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.Address,
                        //IdentityServerConstants.StandardScopes.Email,
                        //IdentityServerConstants.StandardScopes.OfflineAccess,
                        //IdentityServerConstants.StandardScopes.Phone,
                        "jwellApi"
                    },
                    AllowOfflineAccess = true
                },
            };
            // client credentials client
            return clients;
        }
    }
}
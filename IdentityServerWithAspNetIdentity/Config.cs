// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServerWithAspNetIdentity.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerWithAspNetIdentity
{
    public class Config
    {
        private static IIdentityServer4ClientService identityServer4ClientService;
        public Config(IIdentityServer4ClientService _identityServer4ClientService)
        {
            identityServer4ClientService = _identityServer4ClientService;
        }
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
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    //ClientId不能重复
                    ClientId = "mvc",
                    ClientName = "积微循环",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "jwellApi"
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "mvc2",
                    ClientName = "积微云采",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "jwellApi"
                    },
                    AllowOfflineAccess = true
                }
            };
            foreach(var client in clients)
            {
                identityServer4ClientService.InsertClient(client);
            }
            
            //todo client存入数据库
            // client credentials client
            return clients;
        }
    }
}
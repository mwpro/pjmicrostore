// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;
using Identity.Contracts;
using IdentityServer4;
using Microsoft.Extensions.Configuration;

namespace Identity.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource(Scopes.Pjmicrostore)
                {
                    UserClaims = new [] {
                        Scopes.Role,
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName), 
                new ApiResource(Scopes.Carts), 
                new ApiResource(Scopes.Orders), 
                new ApiResource(Scopes.Payments), 
                new ApiResource(Scopes.Identities), 
                new ApiResource(Scopes.Products), 
                new ApiResource(Scopes.Photos), 
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = configuration.GetValue<string>("ClientCredentialClients:Orders:ClientId"),
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(configuration.GetValue<string>("ClientCredentialClients:Orders:ClientSecret").Sha256()) },
                    AllowedScopes = { Scopes.Carts }
                },
                new Client
                {
                    ClientId = configuration.GetValue<string>("ClientCredentialClients:EmailSender:ClientId"),
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(configuration.GetValue<string>("ClientCredentialClients:EmailSender:ClientSecret").Sha256()) },
                    AllowedScopes = { Scopes.Orders }
                },
                new Client
                {
                    ClientId = configuration.GetValue<string>("ClientCredentialClients:IdentityApi:ClientId"),
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(configuration.GetValue<string>("ClientCredentialClients:IdentityApi:ClientSecret").Sha256()) },
                    AllowedScopes = { Scopes.Orders }
                },
                // SPA client using Code flow
                new Client
                {
                    ClientId = FrontNames.FrontStore,
                    ClientName = "Front Store Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    AccessTokenLifetime = 90,
                    IdentityTokenLifetime = 30,

                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = configuration.GetSection("SpaClients:FrontStore:RedirectUris").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),
                    PostLogoutRedirectUris = configuration.GetSection("SpaClients:FrontStore:PostLogoutRedirectUris").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),
                    AllowedCorsOrigins = configuration.GetSection("SpaClients:FrontStore:AllowedCorsOrigins").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        Scopes.Pjmicrostore
                    }
                },
                new Client
                {
                    ClientId = FrontNames.FrontAdmin,
                    ClientName = "Front Admin Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    
                    AccessTokenLifetime = 90,
                    IdentityTokenLifetime = 30,

                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = configuration.GetSection("SpaClients:FrontAdmin:RedirectUris").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),
                    PostLogoutRedirectUris = configuration.GetSection("SpaClients:FrontAdmin:PostLogoutRedirectUris").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),
                    AllowedCorsOrigins = configuration.GetSection("SpaClients:FrontAdmin:AllowedCorsOrigins").AsEnumerable().Where(x => x.Value != null).Select(x => x.Value).ToList(),
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        Scopes.Pjmicrostore
                    }
                }
            }.ToList();
        }
    }
}
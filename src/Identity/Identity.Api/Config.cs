// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using Identity.Contracts;
using IdentityServer4;

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
                new IdentityResource("roles", new[] { "role"} ), 
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
                {
                    UserClaims = new [] { "role" }
                },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName), 
                new ApiResource("carts"), 
                new ApiResource("orders"), 
                new ApiResource("payments"), 
                new ApiResource("identities"), 
                new ApiResource("products"), 
                new ApiResource("photos"), 
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "orders",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("ordersSecret".Sha256())},
                    AllowedScopes = { "carts" }
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

                    RedirectUris =
                    {
                        "http://localhost:8080/callback",
                        "http://localhost:8080/silentRenew"
                    },
                    PostLogoutRedirectUris = {"http://localhost:8080"},
                    AllowedCorsOrigins = {"http://localhost:8080"},
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api1",
                        "roles"
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

                    RedirectUris =
                    {
                        "http://localhost:8081/callback",
                        "http://localhost:8081/silentRenew"
                    },
                    PostLogoutRedirectUris = {"http://localhost:8081"},
                    AllowedCorsOrigins = {"http://localhost:8081"},
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api1",
                        "roles"
                    }
                }
            };
        }
    }
}
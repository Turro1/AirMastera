using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AirMastera.Infrastructure.Identity.Configuration
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("AirMasteraWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("AirMasteraWebAPI", "Web API", new[]
                {JwtClaimTypes.Name})
            {
                Scopes = {"AirMasteraWebAPI"}
            }
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "airmastera-web-api",
                ClientName = "AirMastera Web",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://localhost:5001/signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:5001"
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:5001/signin-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "AirMasteraWebAPI"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
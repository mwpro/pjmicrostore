using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure
{
    public interface IAuthorizationTokenService
    {
        Task<string> GetBearerToken();
    }

    public class AuthorizationTokenService : IAuthorizationTokenService
    {
        private const string DiscoveryData = nameof(AuthorizationTokenService)+ "_" + nameof(DiscoveryData);
        private const string Token = nameof(AuthorizationTokenService)+ "_" + nameof(Token);
        private const int TokenExpirationOffsetSeconds = 5;

        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private DiscoveryResponse _discoveryResponse;
        private readonly HttpClient _client;

        public AuthorizationTokenService(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
            _client = new HttpClient();
        }

        public async Task<string> GetBearerToken()
        {
            if (_memoryCache.TryGetValue(Token, out TokenResponse authToken))
                return authToken.AccessToken;

            return await ObtainNewToken();
        }

        private async Task<string> ObtainNewToken()
        {
            await EnsureDiscoveryData();

            var authToken = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryResponse.TokenEndpoint,
                ClientId = _configuration.GetValue<string>("Identity:ClientId"),
                ClientSecret = _configuration.GetValue<string>("Identity:ClientSecret")
            });

            if (authToken.IsError)
            {
                throw authToken.Exception;
            }

            _memoryCache.Set(Token, authToken, TimeSpan.FromSeconds(authToken.ExpiresIn - TokenExpirationOffsetSeconds));

            return authToken.AccessToken;
        }

        private async Task EnsureDiscoveryData()
        {
            if (!_memoryCache.TryGetValue(DiscoveryData, out _discoveryResponse))
            {
                _discoveryResponse = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
                {
                    Address = _configuration.GetValue<string>("Identity:Authority"),
                    Policy =
                    {
                        RequireHttps = _configuration.GetValue<bool>("Identity:RequireHttpsMetadata")
                    }
                });
                if (_discoveryResponse.IsError)
                {
                    throw _discoveryResponse.Exception;
                }

                _memoryCache.Set(DiscoveryData, _discoveryResponse, new MemoryCacheEntryOptions()
                {
                    Priority = CacheItemPriority.NeverRemove
                });
            }
        }
    }
}
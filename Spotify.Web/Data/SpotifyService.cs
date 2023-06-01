using Microsoft.Extensions.Options;
using SpotifyAPI.Web;

namespace Spotify.Web.Data
{
    public class SpotifyService
    {
        private readonly IOptions<Spotifysettings> _config;

        public SpotifyService(IOptions<Spotifysettings> config)
        {
            _config = config;
        }

        public async Task<string> GetAccessToken(string code)
        {
            var clientId = _config.Value.ClientId;
            var clientSecret = _config.Value.ClientSecret;
            var response = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(clientId, clientSecret, code, new Uri("https://localhost:7053/callback"))
            );
            return response.AccessToken;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace GetNewSong
{
    public class SpotifyHelpers
    {
        public async Task<string> GetAccessToken(HttpClient client, string clientId, string clientSecret)
        {
            var body = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string,string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        };

            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token") { Content = new FormUrlEncodedContent(body) };
            var response = await client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            var test = await response.Content.ReadAsStringAsync();
            SpotifyToken spotifyToken = JsonConvert.DeserializeObject<SpotifyToken>(test);

            return spotifyToken is not null ? spotifyToken.access_token : "";
        }
    }
}

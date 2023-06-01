using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GetNewSong
{
    public class PlayList
    {
        public async Task<spotifyGetInfo> Get(string id, HttpClient client)
        {
            var playlist = await client.GetFromJsonAsync<spotifyGetInfo>($"https://api.spotify.com/v1/playlists/{id}/tracks?fields=items(added_at%2Ctrack(id%2Cname%2Cartists(name)))");

            return playlist ?? new spotifyGetInfo();
        }
    }
}

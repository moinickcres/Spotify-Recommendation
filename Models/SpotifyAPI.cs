using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SpotifyRecommendation.Models
{
    public class SpotifyAPI
    {
        private static readonly string clientId = "980c399c617b47d3a21ac08088217226";
        private static readonly string clientSecret = "80b4280e5617431f8e22e71b9e90393b";

        public static async Task<string> ShowAlbums()
        {
            var token = await GetAccessToken();
            return await GetAlbums(token);
        }

        private static async Task<string> GetAccessToken()
        {
            using var client = new HttpClient();
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

            var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(responseString);
            return json["access_token"].Value<string>();
        }

        private static async Task<String> GetAlbums(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Replace this URL with the specific endpoint you need, e.g., search for albums
            var response = await client.GetAsync("https://api.spotify.com/v1/albums/4aawyAB9vmqN3uQ7FjRGTy");
            var responseString = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(responseString);

            return responseString;
        }
    }
}

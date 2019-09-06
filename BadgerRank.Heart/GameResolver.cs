using BadgerRank.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public class GameResolver
    {
        public async Task<List<Game>> GetGamesForWeek(int year, int week)
        {
            var a = new List<Game>();

            using (var client = CreateCFBApiClient())
            {
                var response = await client.GetAsync(new Uri($"games?year={year}&week={week}", UriKind.Relative));
                if (response.IsSuccessStatusCode)
                {
                    a = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                }
            }

            return a;
        }

        private HttpClient CreateCFBApiClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.collegefootballdata.com/");
            return client;
        }
    }
}

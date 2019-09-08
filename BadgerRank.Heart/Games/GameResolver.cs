using BadgerRank.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public class GameResolver : IGameResolver
    {
        private ICfbApiClientFactory clientFactory;

        public GameResolver(ICfbApiClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Game>> GetGamesForWeek(int year, int week)
        {
            var games = new List<Game>();

            using (var client = this.clientFactory.CreateCFBApiClient())
            {
                var response = await client.GetAsync(new Uri($"games?year={year}&week={week}", UriKind.Relative));
                if (response.IsSuccessStatusCode)
                {
                    games = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                }
            }

            return games;
        }
    }
}

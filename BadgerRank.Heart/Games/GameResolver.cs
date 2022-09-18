using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Games
{
    public class GameResolver : IGameResolver
    {
        private ICfbApiClientFactory clientFactory;

        public GameResolver(ICfbApiClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Game>> GetGamesForYear(int year)
        {
            var weeklyTasks = new List<Task<IEnumerable<Game>>>();

            for(int i = 0; i < 20; i++)
            {
                weeklyTasks.Add(GetGamesForWeek(year, i));
            }

            var weeklyGames = await Task.WhenAll(weeklyTasks);

            return weeklyGames.SelectMany(x => x);
        }

        public async Task<IEnumerable<Game>> GetGamesForWeek(int year, int week)
        {
            var games = new List<Game>();

            var client = this.clientFactory.CreateCFBApiClient();
            var response = await client.GetAsync(new Uri($"games?year={year}&week={week}", UriKind.Relative));
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                games = JsonConvert.DeserializeObject<List<Game>>(responseContent);
            }
            else
            {
                Console.WriteLine();
            }

            return games.ToList();
        }
    }
}

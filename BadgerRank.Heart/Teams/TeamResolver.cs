using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Teams
{
    public sealed class TeamResolver : ITeamResolver
    {
        private ICfbApiClientFactory clientFactory;

        public TeamResolver(ICfbApiClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            var teams = new List<Team>();

            using (var client = this.clientFactory.CreateCFBApiClient())
            {
                var response = await client.GetAsync(new Uri($"teams", UriKind.Relative));
                if (response.IsSuccessStatusCode)
                {
                    teams = JsonConvert.DeserializeObject<List<Team>>(await response.Content.ReadAsStringAsync());
                }
            }

            return teams.ToList();
        }

        public async Task<IEnumerable<Team>> GetFbsTeams()
        {
            var teams = new List<Team>();

            using (var client = this.clientFactory.CreateCFBApiClient())
            {
                var response = await client.GetAsync(new Uri($"teams/fbs", UriKind.Relative));
                if (response.IsSuccessStatusCode)
                {
                    teams = JsonConvert.DeserializeObject<List<Team>>(await response.Content.ReadAsStringAsync());
                }
            }

            return teams.ToList();
        }
    }
}

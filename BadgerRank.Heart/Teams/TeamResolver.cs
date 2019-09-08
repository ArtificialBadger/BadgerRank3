using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BadgerRank.Core.Models;
using Newtonsoft.Json;

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

            return teams;
        }
    }
}

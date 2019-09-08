using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Drives
{
    public sealed class DriveResolver : IDriveResolver
    {
        private ICfbApiClientFactory clientFactory;

        public DriveResolver(ICfbApiClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Drive>> GetDrives(DriveParameters parameters)
        {
            var drives = new List<Drive>();

            using (var client = this.clientFactory.CreateCFBApiClient())
            {
                var queryParameters = new Dictionary<string, string>();

                if (!String.IsNullOrWhiteSpace(parameters.SeasonType))
                    queryParameters["seasonType"] = parameters.SeasonType;

                if (parameters.Year.HasValue)
                    queryParameters["year"] = parameters.Year.ToString();

                if (parameters.Week.HasValue)
                    queryParameters["week"] = parameters.Week.ToString();

                if (!String.IsNullOrWhiteSpace(parameters.Team))
                    queryParameters["team"] = parameters.Team;

                var url = "drives".BuildWithQueryParameters(queryParameters);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    drives = JsonConvert.DeserializeObject<List<Drive>>(await response.Content.ReadAsStringAsync());
                }
            }

            return drives.ToList();
        }
    }
}

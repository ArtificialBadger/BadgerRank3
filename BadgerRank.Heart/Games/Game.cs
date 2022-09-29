using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadgerRank.Heart.Games
{
    public class Game
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("week")]
        public long Week { get; set; }

        [JsonProperty("season_type")]
        public string SeasonType { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("neutral_site")]
        public bool NeutralSite { get; set; }

        [JsonProperty("conference_game")]
        public bool? ConferenceGame { get; set; }

        [JsonProperty("attendance")]
        public long? Attendance { get; set; }

        [JsonProperty("venue_id")]
        public long? VenueId { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("home_team")]
        public string HomeTeam { get; set; }

        [JsonProperty("home_conference")]
        public string HomeConference { get; set; }

        [JsonProperty("home_points")]
        public int? HomePoints { get; set; }

        [JsonProperty("home_line_scores")]
        public long[] HomeLineScores { get; set; }

        [JsonProperty("away_team")]
        public string AwayTeam { get; set; }

        [JsonProperty("away_conference")]
        public string AwayConference { get; set; }

        [JsonProperty("away_points")]
        public int? AwayPoints { get; set; }

        [JsonProperty("away_line_scores")]
        public long[] AwayLineScores { get; set; }
    }
}

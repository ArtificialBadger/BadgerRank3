using Newtonsoft.Json;

namespace BadgerRank.Heart.Drives
{
    public sealed class Drive
    {
        [JsonProperty("offense")]
        public string Offense { get; set; }

        [JsonProperty("offense_conference")]
        public string OffenseConference { get; set; }

        [JsonProperty("defense")]
        public string Defense { get; set; }

        [JsonProperty("defense_conference")]
        public string DefenseConference { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("game_id")]
        public long GameId { get; set; }

        [JsonProperty("scoring")]
        public bool Scoring { get; set; }

        [JsonProperty("start_period")]
        public long StartPeriod { get; set; }

        [JsonProperty("start_yardline")]
        public long StartYardline { get; set; }

        [JsonProperty("start_time")]
        public Time StartTime { get; set; }

        [JsonProperty("end_period")]
        public long EndPeriod { get; set; }

        [JsonProperty("end_yardline")]
        public long EndYardline { get; set; }

        [JsonProperty("end_time")]
        public Time EndTime { get; set; }

        [JsonProperty("plays")]
        public long Plays { get; set; }

        [JsonProperty("yards")]
        public long Yards { get; set; }

        [JsonProperty("drive_result")]
        public string DriveResult { get; set; }
    }

    public sealed class Time
    {
        [JsonProperty("minutes")]
        public long Minutes { get; set; }

        [JsonProperty("seconds")]
        public long Seconds { get; set; }
    }
}

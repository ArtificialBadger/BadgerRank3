using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadgerRank.Heart.Teams
{
    public sealed class Team
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("mascot")]
        public string Mascot { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("alt_name_1")]
        public string AltName1 { get; set; }

        [JsonProperty("alt_name_2")]
        public string AltName2 { get; set; }

        [JsonProperty("alt_name_3")]
        public string AltName3 { get; set; }

        [JsonProperty("conference")]
        public string Conference { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("alt_color")]
        public string AltColor { get; set; }

        [JsonProperty("logos")]
        public string[] Logos { get; set; }

        public bool IsP5 => Constants.P5Conferences.Contains(this.Conference);

        public bool IsFbs => Constants.FbsConferences.Contains(this.Conference);

    }
}

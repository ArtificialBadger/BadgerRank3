using System;
using System.Collections.Generic;
using System.Text;

namespace BadgerRank.Heart
{
    public static class Constants
    {
        public static HashSet<string> P5Conferences = new HashSet<string>(new List<string>() { "Big Ten", "SEC", "ACC", "Pac-12", "Big 12" });

        public static HashSet<string> FbsConferences = new HashSet<string>(new[] { "Big Ten", "SEC", "ACC", "Pac-12", "Big 12", "FBS Independents", "Mountain West", "Mid-American", "American Athletic", "Conference USA", "Sun Belt" });
    }
}

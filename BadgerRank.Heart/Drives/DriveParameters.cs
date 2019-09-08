namespace BadgerRank.Heart.Drives
{
    public sealed class DriveParameters
    {
        public string SeasonType { get; set; } = "regular";

        public int? Year { get; set; }

        public int? Week { get; set; }

        public string Team { get; set; }
    }
}

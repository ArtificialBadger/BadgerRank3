using BadgerRank.Heart.Drives;
using BadgerRank.Heart.Games;
using BadgerRank.Heart.Teams;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public class Ranker
    {
        private IDriveResolver driveResolver;
        private IGameResolver gameResolver;
        private ITeamResolver teamResolver;

        public Ranker(IDriveResolver driveResolver, IGameResolver gameResolver, ITeamResolver teamResolver)
        {
            this.driveResolver = driveResolver;
            this.gameResolver = gameResolver;
            this.teamResolver = teamResolver;
        }

        public async Task<string> Rank()
        {
            var week = 1;
            var year = 2019;

            var resultBuilder = new StringBuilder();

            var teams = await this.teamResolver.GetFbsTeams();
            var drives = await this.driveResolver.GetDrives(new DriveParameters() { Week = week, Year = year });

            foreach (var team in teams)
            {
                resultBuilder.AppendLine($"{team.School} {team.IsP5}");
            }

            resultBuilder.AppendLine($"{drives.First()}");
            resultBuilder.AppendLine($"{drives.Count()}");

            return resultBuilder.ToString();
        }
    }
}

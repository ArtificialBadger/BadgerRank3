using BadgerRank.Heart.Drives;
using BadgerRank.Heart.Games;
using BadgerRank.Heart.Teams;
using System;
using System.Collections.Generic;
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
            var teamDictionary = new Dictionary<string, TeamDominance>();

            //var week = 1;
            var year = 2019;

            var resultBuilder = new StringBuilder();

            var teams = await this.teamResolver.GetTeams();

            foreach (var team in teams)
            {
                teamDictionary[team.School] = new TeamDominance() { Team = team, TotalDominance = 0m, GamesPlayed = 0 };
            }

            for (int week = 1; week < 18; week++)
            {
                var games = await this.gameResolver.GetGamesForWeek(year, week);
                "".ToString();
                foreach (var game in games)
                {
                    if (!game.HomePoints.HasValue || !game.AwayPoints.HasValue)
                    {
                        //Console.WriteLine($"What happened with {game.HomeTeam} and {game.AwayTeam}");
                        continue;
                    }

                    if (game.HomePoints.Value == 0 && game.AwayPoints == 0)
                    {
                        continue;
                    }

                    TeamDominance winner, loser;

                    if (game.HomePoints > game.AwayPoints)
                    {
                        winner = teamDictionary[game.HomeTeam];
                        loser = teamDictionary[game.AwayTeam];

                        var newWinnerDominance = CalculateDominance(winner, loser, game.HomePoints.Value - game.AwayPoints.Value);
                        var newLoserDominance = CalculateDominance(loser, winner, game.AwayPoints.Value - game.HomePoints.Value);

                        Console.WriteLine($"{winner.Team.School} beats {loser.Team.School} {game.HomePoints}-{game.AwayPoints} with dominance {newWinnerDominance.TotalDominance - winner.TotalDominance} | {newLoserDominance.TotalDominance - loser.TotalDominance}");

                        teamDictionary[game.HomeTeam] = newWinnerDominance;
                        teamDictionary[game.AwayTeam] = newLoserDominance;
                    }
                    else
                    {
                        winner = teamDictionary[game.AwayTeam];
                        loser = teamDictionary[game.HomeTeam];

                        var newWinnerDominance = CalculateDominance(winner, loser, game.AwayPoints.Value - game.HomePoints.Value);
                        var newLoserDominance = CalculateDominance(loser, winner, game.HomePoints.Value - game.AwayPoints.Value);

                        Console.WriteLine($"{winner.Team.School} beats {loser.Team.School} {game.AwayPoints}-{game.HomePoints} with dominance {newWinnerDominance.TotalDominance - winner.TotalDominance} | {newLoserDominance.TotalDominance - loser.TotalDominance}");

                        teamDictionary[game.AwayTeam] = newWinnerDominance;
                        teamDictionary[game.HomeTeam] = newLoserDominance;
                    }
                }
            }

            var rankedTeams = teamDictionary.Select(x => x.Value).Where(x => x.Team.IsFbs).OrderByDescending(x => x.DominancePerGame);

            return Format(rankedTeams);
        }

        private string Format(IOrderedEnumerable<TeamDominance> rankedTeams)
        {
            var rankList = rankedTeams.ToList();

            var builder = new StringBuilder();
            builder.AppendLine("Top Ranked Teams");

            var longestTeamName = rankList.Max(x => x.Team.School.Length);

            for (int i = 0; i < rankList.Count; i++)
            {
                builder.AppendLine($"{i+1, 2}. {rankList[i].Team.School.PadRight(longestTeamName + 1, ' ')} {rankList[i].DominancePerGame.ToString("F2")}");
            }

            return builder.ToString();
        }

        private TeamDominance CalculateDominance(TeamDominance teamDominance, TeamDominance opponent, int marginOfVictory)
        {
            var max = marginOfVictory > 0 ? 1.0m : 0.5m;
            var min = marginOfVictory > 0 ? 0.5m : 0.0m;

            var dominanceConstant = marginOfVictory > 0 ? CalculateDominanceConstant(opponent.Team, marginOfVictory) : -1 * CalculateDominanceConstant(opponent.Team, marginOfVictory);
            var gameDominance = Math.Min(max, Math.Max(min, .5m + (.25m * opponent.DominancePerGame) + dominanceConstant));

            return new TeamDominance()
            {
                GamesPlayed = teamDominance.GamesPlayed + 1,
                TotalDominance = teamDominance.TotalDominance + gameDominance,
                Team = teamDominance.Team,
            };
        }

        private decimal CalculateDominanceConstant(Team team, int marginOfVictory)
        {
            decimal marginBonus;

            if (marginOfVictory < 8)
            {
                marginBonus = 0.2m;
            }
            else if (marginOfVictory < 14)
            {
                marginBonus = 0.3m;
            }
            else if (marginOfVictory < 21)
            {
                marginBonus = 0.4m;
            }
            else
            {
                marginBonus = 0.5m;
            }

            if (team.IsP5)
            {
                marginBonus *= 1.0m;
            }
            else if (team.IsFbs)
            {
                marginBonus *= 0.8m;
            }
            else
            {
                marginBonus *= 0.5m;
            }

            return marginBonus;
        }

        //    public async Task<string> Rank()
        //    {
        //        var teamDictionary = new Dictionary<string, TeamDominance>();

        //        var week = 1;
        //        var year = 2019;

        //        var resultBuilder = new StringBuilder();


        //        var teams = await this.teamResolver.GetFbsTeams();
        //        var drives = await this.driveResolver.GetDrives(new DriveParameters() { Week = week, Year = year });

        //        var gameDrives = drives.GroupBy(d => d.GameId);

        //        foreach (var team in teams)
        //        {
        //            teamDictionary[team.School] = new TeamDominance() { Team = team, TotalDominance = 0.0m };
        //        }

        //        foreach (var gameDrive in gameDrives)
        //        {
        //            var orderedDrives = gameDrive.OrderBy(g => g.StartPeriod).ThenByDescending(g => g.StartTime).ToList();

        //            var teamOne = teamDictionary[orderedDrives[0].Offense];
        //            var teamTwo = teamDictionary[orderedDrives[0].Defense];

        //            int team1Net = 0;
        //            int team2Net = 0;

        //            foreach (var drive in orderedDrives)
        //            {   
        //                if(drive.Scoring)
        //                {
        //                    if (drive.Offense == teamOne.Team.School)
        //                    {
        //                        team1Net +=     
        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //            }
        //        }

        //        //foreach (var team in teams)
        //        //{
        //        //    resultBuilder.AppendLine($"{team.School} {team.IsP5}");
        //        //}

        //        //resultBuilder.AppendLine($"{drives.First()}");
        //        //resultBuilder.AppendLine($"{drives.Count()}");

        //        return resultBuilder.ToString();
        //    }
        //}

        public struct TeamDominance
        {
            public Team Team { get; set; }

            public decimal TotalDominance { get; set; }

            public int GamesPlayed { get; set; }

            public decimal DominancePerGame => GamesPlayed == 0 ? .5m : TotalDominance / GamesPlayed;

        }
    }
}

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
            
            var year = 2022;

            var resultBuilder = new StringBuilder();

            var teams = await this.teamResolver.GetTeams();

            foreach (var team in teams)
            {
                teamDictionary[team.School] = new TeamDominance() { Team = team, TotalDominance = 0m, GamesPlayed = 0 };
            }

            var games = await this.gameResolver.GetGamesForYear(year);

            await RunSeason(games, teamDictionary, year);
            await RunSeason(games, teamDictionary, year);
            await RunSeason(games, teamDictionary, year);

            var rankedTeams = teamDictionary.Select(x => x.Value).Where(x => x.Team.IsFbs).OrderByDescending(x => x.DominancePerGame);

            return Format(rankedTeams, year);
        }

        private async Task RunSeason(IEnumerable<Game> games, Dictionary<string, TeamDominance> teamDictionary, int year)
        {
            var weeks = games.GroupBy(g => g.Week).OrderBy(g => g.Key);
            foreach (var week in weeks)
            {
                await RunWeek(week, teamDictionary, year);
            }
        }

        private async Task RunWeek(IEnumerable<Game> weeklyGames, Dictionary<string, TeamDominance> teamDictionary, int year)
        {
            RunGames(teamDictionary, weeklyGames);
        }

        private void RunGames(Dictionary<string, TeamDominance> teamDictionary, IEnumerable<Game> games)
        {

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
                TeamDominance newWinnerDominance, newLoserDominance;

                if (game.HomePoints > game.AwayPoints)
                {
                    winner = teamDictionary[game.HomeTeam];
                    loser = teamDictionary[game.AwayTeam];

                    newWinnerDominance = CalculateDominance(winner, loser, game.HomePoints.Value - game.AwayPoints.Value);
                    newLoserDominance = CalculateDominance(loser, winner, game.AwayPoints.Value - game.HomePoints.Value);

                    teamDictionary[game.HomeTeam] = newWinnerDominance;
                    teamDictionary[game.AwayTeam] = newLoserDominance;
                }
                else
                {
                    winner = teamDictionary[game.AwayTeam];
                    loser = teamDictionary[game.HomeTeam];

                    newWinnerDominance = CalculateDominance(winner, loser, game.AwayPoints.Value - game.HomePoints.Value);
                    newLoserDominance = CalculateDominance(loser, winner, game.HomePoints.Value - game.AwayPoints.Value);

                    teamDictionary[game.AwayTeam] = newWinnerDominance;
                    teamDictionary[game.HomeTeam] = newLoserDominance;
                }

                Console.WriteLine($"{game.Week}: {((winner.Team.Abbreviation ?? winner.Team.School) + $" ({winner.DominancePerGame:F4})").PadLeft(14)} beats {((loser.Team.Abbreviation ?? loser.Team.School) .ToString().PadRight(5) + $" ({loser.DominancePerGame:F4})")} {Math.Max(game.HomePoints.Value, game.AwayPoints.Value).ToString("D2")}-{Math.Min(game.HomePoints.Value, game.AwayPoints.Value).ToString("D2")} with dominance {(newWinnerDominance.TotalDominance - winner.TotalDominance):F4} | {(newLoserDominance.TotalDominance - loser.TotalDominance):F4}");
            }
        }

        private string Format(IOrderedEnumerable<TeamDominance> rankedTeams, int year)
        {
            var rankList = rankedTeams.ToList();

            var builder = new StringBuilder();
            builder.AppendLine($"Top Ranked Teams ({year} season)");

            var longestTeamName = rankList.Max(x => x.Team.School.Length);

            for (int i = 0; i < rankList.Count; i++)
            {
                builder.AppendLine($"{i+1, 3}. {rankList[i].Team.School.PadRight(longestTeamName + 1, ' ')} {rankList[i].DominancePerGame:F4}");
            }

            return builder.ToString();
        }

        private TeamDominance CalculateDominance(TeamDominance teamDominance, TeamDominance opponent, int marginOfVictory)
        {
            var max = marginOfVictory > 0 ? 1.0m : 0.5m;
            var min = marginOfVictory > 0 ? 0.5m : 0.0m;

            var dc = marginOfVictory > 0 ? CalculateWinnerDominanceConstant(teamDominance.Team, opponent.Team, marginOfVictory) : CalculateLoserDominanceConstant(opponent.Team, teamDominance.Team, -1 * marginOfVictory);
            var dominanceConstant = dc;
            var gameDominance = Math.Min(max, Math.Max(min ,(.3m * opponent.DominancePerGame) + dominanceConstant));

            return new TeamDominance()
            {
                GamesPlayed = teamDominance.GamesPlayed + 1,
                TotalDominance = teamDominance.TotalDominance + gameDominance,
                Team = teamDominance.Team,
            };
        }

        private decimal CalculateWinnerDominanceConstant(Team winner, Team loser, int marginOfVictory)
        {
            decimal marginBonus;

            if (marginOfVictory < 8)
            {
                marginBonus = 0.65m;
            }
            else if (marginOfVictory < 14)
            {
                marginBonus = 0.7m;
            }
            else if (marginOfVictory < 21)
            {
                marginBonus = 0.75m;
            }
            else
            {
                marginBonus = 0.85m;
            }

            if (loser.IsP5)
            {
                marginBonus *= .9m;
            }
            else if (loser.IsFbs)
            {
                marginBonus *= .8m;
            }
            else
            {
                marginBonus *= .7m;
            }
            
            return marginBonus;
        }

        private decimal CalculateLoserDominanceConstant(Team winner, Team loser, int marginOfLoss)
        {
            decimal marginBonus;

            if (marginOfLoss < 8)
            {
                marginBonus = 0.3m;
            }
            else if (marginOfLoss < 14)
            {
                marginBonus = 0.2m;
            }
            else if (marginOfLoss < 21)
            {
                marginBonus = 0.1m;
            }
            else
            {
                marginBonus = 0.0m;
            }

            if (winner.IsP5)
            {
                marginBonus *= 1m;
            }
            else if (winner.IsFbs)
            {
                marginBonus *= .8m;
            }
            else
            {
                marginBonus *= 0m;
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

            public decimal DominancePerGame => GamesPlayed == 0 ? this.Team.BaseDominance() : TotalDominance / GamesPlayed;
        }
    }

    public static class TeamExtentions
    {
        public static decimal BaseDominance(this Team team)
        {
            if (team.IsP5)
            {
                return .6m;
            }
            else if (team.IsFbs)
            {
                return .5m;
            }
            else
            {
                return .25m;
            }
        }
    }
}

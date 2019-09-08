using BadgerRank.Heart.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public class Ranker
    {
        private IGameResolver gameResolver;
        private ITeamResolver teamResolver;

        public async Task Rank()
        {
            var games = await this.gameResolver.GetGamesForWeek(2019, 1);
        }
    }
}

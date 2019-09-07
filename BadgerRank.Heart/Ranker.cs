using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public class Ranker
    {
        private IGameResolver gameResolver;

        public Ranker()
        {
            this.gameResolver = new GameResolver();
        }

        public async Task Rank()
        {
            var teams = await this.
            var games = await this.gameResolver.GetGamesForWeek(2019, 1);
        }
    }
}

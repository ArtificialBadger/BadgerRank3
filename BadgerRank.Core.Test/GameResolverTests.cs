using System;
using System.Threading.Tasks;
using Xunit;

namespace BadgerRank.Heart.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GamesCanBeResolved()
        {
            var resolver = new GameResolver();

            var games = await resolver.GetGamesForWeek(2019, 1);

            Assert.True(games.Count > 0);
        }
    }
}

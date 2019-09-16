using BadgerRank.Heart.Drives;
using BadgerRank.Heart.Games;
using BadgerRank.Heart.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BadgerRank.Heart.Test
{
    public class RankerTests
    {
        [Fact]
        public async Task DoThing()
        {
            var clientApiFactory = new CfbApiClientFactory();

            var driveResolver = new DriveResolver(clientApiFactory);
            var gameResolver = new GameResolver(clientApiFactory);
            var teamResolver = new TeamResolver(clientApiFactory);

            var ranker = new Ranker(driveResolver, gameResolver, teamResolver);

            var rank = await ranker.Rank();
            Assert.True(rank.Length > 100);
        }
    }
}

using BadgerRank.Heart.Games;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BadgerRank.Heart.Test
{
    public class GameResolverTests
    {
        private Mock<ICfbApiClientFactory> clientFactory;

        private GameResolver resolver;
        
        public GameResolverTests()
        {
            this.clientFactory = new Mock<ICfbApiClientFactory>();

            this.resolver = new GameResolver(new CfbApiClientFactory());
        }

        [Fact]
        public async Task GamesCanBeResolved()
        {
            var games = await this.resolver.GetGamesForWeek(2019, 1);

            Assert.True(games.Count() > 0);
        }

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Games
{
    public interface IGameResolver
    {
        Task<IEnumerable<Game>> GetGamesForWeek(int year, int week);
    }
}

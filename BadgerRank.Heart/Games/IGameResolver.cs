using BadgerRank.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart
{
    public interface IGameResolver
    {
        Task<IEnumerable<Game>> GetGamesForWeek(int year, int week);
    }
}

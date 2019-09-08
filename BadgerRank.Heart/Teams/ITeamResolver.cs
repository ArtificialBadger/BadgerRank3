using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Teams
{
    public interface ITeamResolver
    {
        Task<IEnumerable<Team>> GetTeams();

        Task<IEnumerable<Team>> GetFbsTeams();
    }
}

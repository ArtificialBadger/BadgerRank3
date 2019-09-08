using BadgerRank.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Teams
{
    public interface ITeamResolver
    {
        Task<IEnumerable<Team>> GetTeams();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadgerRank.Heart.Drives
{
    public interface IDriveResolver
    {
        Task<IEnumerable<Drive>> GetDrives(DriveParameters parameters);
    }
}

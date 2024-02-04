using DAW.Modells;
using System.Diagnostics.Metrics;

namespace Daw.Interfaces
{
    public interface SERVERInterface
    {
        ICollection<SERVER> GetServers();
        SERVER GetSERVER(int id);
        SERVER GetSERVERByPlayer(int PlayerId);
        ICollection<Player> GetPlayersFromASERVER(int SERVERId);
        bool SERVERExists(int id);
        bool CreateSERVER(SERVER SERVER);
        bool UpdateSERVER(SERVER SERVER);
        bool DeleteSERVER(SERVER SERVER);
        bool Save();
    }
}

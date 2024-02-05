using DAW.Modells;

namespace Daw.Interfaces
{
    public interface PlayerInterface
    {
        ICollection<Player> GetPlayers();
        Player GetPlayer(int PlayerId);
        ICollection<Player> GetPlayerOfAGame(int pokeId);
        ICollection<Game> GetGameByPlayer(int PlayerId);
        bool PlayerExists(int PlayerId);
        bool CreatePlayer(Player Player);
        bool UpdatePlayer(Player Player);
        bool DeletePlayer(Player Player);
        bool Save();
    }
}

using DAW.Modells;

namespace DAW.Interfaces
{
    public interface IgameInterface
    {
        ICollection<Game> GetGames();

        Game GetGame(int id);
        Game GetGame(string name);


        decimal GetScore(int gameid);

        bool GameExists(int gameid);




    }
}

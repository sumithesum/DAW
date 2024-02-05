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

        bool CreateGame(Game game);

        bool Save();

        public bool UpdateGame(Game game);

        public bool DeleteGame(Game game);



    }
}

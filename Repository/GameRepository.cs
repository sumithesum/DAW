using DAW.Data;
using DAW.Interfaces;
using DAW.Modells;

namespace DAW.Repository
{
    public class GameRepository :IgameInterface
    {
        private readonly DataContext _context;
        public GameRepository(DataContext context) {
            _context = context;
        }

        public bool GameExists(int gameid)
        {
            return _context.Games.Any(p => p.ID == gameid);
        }

        public Game GetGame(int id)
        {
            return _context.Games.Where(p => p.ID == id).FirstOrDefault();
        }

        public Game GetGame(string name)
        {
            return _context.Games.Where(p => p.Name == name).FirstOrDefault();
        }


        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(x => x.ID).ToList();
        }

        public decimal GetScore(int gameid)
        {
            var rviews = _context.Reviews.Where(p =>p.ID == gameid);

            if(rviews.Count() <= 0 )
                return 0;

            return ((decimal)rviews.Sum(r => r.rating) /rviews.Count());
        }
    }
}

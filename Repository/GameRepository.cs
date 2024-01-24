using DAW.Data;
using DAW.Modells;

namespace DAW.Repository
{
    public class GameRepository
    {
        private readonly DataContext _context;
        public GameRepository(DataContext context) {
            _context = context;
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(x => x.ID).ToList();
        }
    }
}

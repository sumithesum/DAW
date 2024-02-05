using AutoMapper;
using Daw.Interfaces;
using DAW.Data;
using DAW.Modells;

namespace Daw.Repository
{
    public class PlayerRepository:PlayerInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PlayerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreatePlayer(Player Player)
        {
            _context.Add(Player);
            return Save();
        }

        public bool DeletePlayer(Player Player)
        {
            _context.Remove(Player);
            return Save();
        }

        public ICollection<Game> GetGameByPlayer(int PlayerId)
        {
            return _context.GamePlayers.Where(p => p.Game.ID == PlayerId).Select(p => p.Game).ToList();
        }

        public Player GetPlayer(int PlayerId)
        {
            return _context.Players.Where(p => p.ID == PlayerId).FirstOrDefault();

        }

        public ICollection<Player> GetPlayerOfAGame(int playerId)
        {
            return _context.GamePlayers.Where(p => p.Game.ID == playerId).Select(o => o.Player).ToList();
        }

        public ICollection<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public bool PlayerExists(int PlayerId)
        {
            return _context.Players.Any(p => p.ID == PlayerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePlayer(Player Player)
        {
            _context.Update(Player);
            return Save();
        }
    }
}

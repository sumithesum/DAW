using AutoMapper;
using Daw.Interfaces;
using DAW.Data;
using DAW.Modells;
using System.Diagnostics.Metrics;

namespace Daw.Repository
{
    public class ServerRepository:SERVERInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ServerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateSERVER(SERVER SERVER)
        {
            _context.Add(SERVER);
            return Save();
        }

        public bool DeleteSERVER(SERVER SERVER)
        {
            _context.Remove(SERVER);
            return Save();
        }

        public ICollection<SERVER> GetServers()
        {
            return _context.Servers.ToList();
        }

        public ICollection<Player> GetPlayersFromASERVER(int SERVERId)
        {
            return _context.Players.Where(c => c.Server.ID == SERVERId).ToList();
        }

        public SERVER GetSERVER(int id)
        {
            return _context.Servers.Where(p => p.ID == id).FirstOrDefault();
        }

        public SERVER GetSERVERByPlayer(int playerId)
        {
            return _context.Players.Where(o => o.ID == playerId).Select(c => c.Server).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SERVERExists(int id)
        {
            return _context.Servers.Any(p => p.ID == id);
        }

        public bool UpdateSERVER(SERVER SERVER)
        {
            _context.Update(SERVER);
            return Save();
        }
    }
}

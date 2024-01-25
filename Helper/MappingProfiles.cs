using AutoMapper;
using Daw.DTO;
using DAW.Modells;

namespace Daw.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles() {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
}

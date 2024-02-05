using AutoMapper;
using Daw.DTO;
using DAW.Modells;

namespace Daw.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<SERVER, ServerDto>();
            CreateMap<ServerDto,SERVER>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Reviewer, ReviwerDto>();
            CreateMap<ReviwerDto, Reviewer>();
        }
    }
}

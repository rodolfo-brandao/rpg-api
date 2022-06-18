using AutoMapper;
using Rpg.Application.Responses.Auth;
using Rpg.Core.Models;

namespace Rpg.Application.MapperProfiles
{
    internal class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Player, LoginResponse>();
        }
    }
}

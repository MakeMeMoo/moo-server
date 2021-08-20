using AutoMapper;
using moo_server.Core.Entities;

namespace moo_server.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}

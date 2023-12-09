using AutoMapper;
using Entity;

namespace Bussiness.Automapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<Tenant, Tenant>();

        }
    }
}
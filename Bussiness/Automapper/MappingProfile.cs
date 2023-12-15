using AutoMapper;
using Entity;
using Entity.DTOs;

namespace Bussiness.Automapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<Tenant, Tenant>();
            CreateMap<Bill,Bill>();

            CreateMap<BillDTO, Bill>();
            CreateMap<Bill,BillDTO>();

            CreateMap<TenantDTO, Tenant>();
            CreateMap<Tenant,TenantDTO>();
        }
    }
}
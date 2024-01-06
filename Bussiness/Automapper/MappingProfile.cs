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

            CreateMap<BillDTO, Bill>().ReverseMap();

            CreateMap<TenantDTO, Tenant>().ReverseMap();

            CreateMap<Manager, Manager>();

            CreateMap<ApartmentDTO,Apartment>().ReverseMap();
        }
    }
}
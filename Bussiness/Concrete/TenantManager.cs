using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Concrete
{
    public class TenantManager : ITenantService
    {
        private readonly ITenantDal tenantDal;
        private readonly IMapper mapper;

        public TenantManager(ITenantDal tenantDal, IMapper mapper)
        {
            this.tenantDal = tenantDal;
            this.mapper = mapper;
        }

        public Result Add(TenantDTO tenantDTO)
        {
            Tenant tenant = mapper.Map<Tenant>(tenantDTO);
            tenantDal.Add(tenant);
            return new Result(true, "Tenant Added Successfully");
        }

        public Result Delete(Tenant tenant)
        {
            tenantDal.Delete(tenant);
            return new Result(true, "Tenant Deleted Successfully");
        }

        public Result DeleteById(int id)
        {
            var result = GetById(id);
            if (result.Success == false)
                return new Result(false, "There is no Tenant with this id");
            return Delete(result.Data);
        }

        public DataResult<TenantDTO> GetAsDTOById(int id)
        {
            var tenantResult = GetById(id);
            if (tenantResult.Success == false)
                return new DataResult<TenantDTO>(false, "There is no Tenant with this id", null);
            var tenantDTO = mapper.Map<TenantDTO>(tenantResult.Data);
            return new DataResult<TenantDTO>(true, "Tenant successfully retrieved", tenantDTO);
        }

        public DataResult<Tenant> GetById(int id)
        {
            var tenant = tenantDal.Get(t => t.Id == id);
            if (tenant != null)
                return new DataResult<Tenant>(true, "Tenant successfully retrieved", tenant);
            return new DataResult<Tenant>(false, "Tenant doesnt found", tenant);
        }

        public DataResult<Tenant> GetByMail(string mail)
        {
            var tenant = tenantDal.Get(t => t.Mail == mail);
            if (tenant != null)
                return new DataResult<Tenant>(true, "Tenant successfully retrieved", tenant);
            return new DataResult<Tenant>(false, "Tenant doesnt found", tenant);
        }

        public DataResult<List<Tenant>> GetList()
        {
            var tenants = tenantDal.GetList();
            return new DataResult<List<Tenant>>(true, "Tenants successfully listed", tenants.ToList());

        }

        public DataResult<TenantVM> GetTenantVMById(int id)
        {
            var tenant = tenantDal.GetTenantVMById(id);
            if (tenant != null)
                return new DataResult<TenantVM>(true, "Tenant successfully retrieved", tenant);
            else
                return new DataResult<TenantVM>(false, "Tenant doesnt found", tenant);
        }

        public DataResult<List<TenantVM>> GetTenantVMs(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false)
        {
            var tenantVMs = tenantDal.GetTenantVMs(apartmentComplexId, blockName, nameFilter, onlyHasDebt);
            return new DataResult<List<TenantVM>>(true, "Tenants successfully listed", tenantVMs);

        }

        public DataResult<Tenant> Login(LoginDTO loginDTO)
        {
            var tenant = GetByMail(loginDTO.Mail);
            if (tenant.Success == false)
                return new DataResult<Tenant>(false, "There is no user with this mail.", null);
            if (tenant.Data.Password != loginDTO.Password)
                return new DataResult<Tenant>(false, "Password is wrong", null);
            return new DataResult<Tenant>(true, "Tenant successfully logged in", tenant.Data);
        }

        public Result Update(TenantDTO tenantDTO)
        {
            var tenantFromDb = GetById(tenantDTO.Id);
            if (tenantFromDb.Success == false)
                return new Result(false, "Tenant doesnt found");

            mapper.Map<TenantDTO, Tenant>(tenantDTO, tenantFromDb.Data);
            tenantDal.Update(tenantFromDb.Data);
            return new Result(true, "Tenant updated successfully");

        }
    }
}

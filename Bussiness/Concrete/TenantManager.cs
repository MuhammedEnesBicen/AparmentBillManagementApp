using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
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
            return new Result(true,"Tenant Added Successfully");
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

        public DataResult<Tenant> GetById(int id)
        {
            var tenant = tenantDal.Get(t=>t.Id == id);
            if (tenant != null)
                return new DataResult<Tenant>(true, "Tenant successfully retrieved", tenant);
            return new DataResult<Tenant>(false, "Tenant doesnt found", tenant);
        }

        public DataResult<List<Tenant>> GetList()
        {
            var tenants = tenantDal.GetList();
            return new DataResult<List<Tenant>>(true, "Tenants successfully listed", tenants.ToList());

        }

        public DataResult<List<TenantVM>> GetTenantVMs(string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false)
        {
            var tenantVMs = tenantDal.GetTenantVMs(blockName, nameFilter, onlyHasDebt);
            return new DataResult<List<TenantVM>>(true, "Tenants successfully listed", tenantVMs);

        }

        public Result Update(Tenant tenant)
        {
            var tenantFromDb = GetById(tenant.Id);
            if (tenantFromDb.Success == false)
                return new Result(false, "Tenant doesnt found");

            mapper.Map<Tenant,Tenant>(tenant,tenantFromDb.Data);
            tenantDal.Update(tenantFromDb.Data);
            return new Result(true, "Tenant updated successfully");

        }
    }
}

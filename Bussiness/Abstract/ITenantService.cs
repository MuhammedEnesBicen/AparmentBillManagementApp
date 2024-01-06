using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Abstract
{
    public interface ITenantService
    {
        Result Add(TenantDTO tenantDTO);
        Result Delete(Tenant tenant);
        Result Update(TenantDTO tenantDTO);
        Result DeleteById(int id);

        DataResult<List<Tenant>> GetList();
        DataResult<Tenant> GetById(int id);
        DataResult<TenantDTO> GetAsDTOById(int id);

        DataResult<Tenant> GetByMail(string mail);

        DataResult<Tenant> Login(LoginDTO loginDTO);

        DataResult<List<TenantVM>> GetTenantVMs(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);
        DataResult<TenantVM> GetTenantVMById(int id);


    }
}

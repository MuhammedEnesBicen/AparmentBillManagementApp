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
        Result Update(Tenant tenant);
        Result DeleteById(int id);

        DataResult<List<Tenant>> GetList();
        DataResult<Tenant> GetById(int id);

        DataResult<List<TenantVM>> GetTenantVMs(string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);

    }
}

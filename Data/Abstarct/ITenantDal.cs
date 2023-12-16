using Core.DataAccess;
using Entity;
using Entity.ViewModels;

namespace DataAccess.Abstarct
{
    public interface ITenantDal : IEntityRepository<Tenant>
    {
        List<TenantVM> GetTenantVMs(string? blockName =null, string? nameFilter = null, bool onlyHasDebt =false);
    }
}

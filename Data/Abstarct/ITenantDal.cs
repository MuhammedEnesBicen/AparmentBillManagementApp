using Core.DataAccess;
using Core.Utilities;
using Entity;
using Entity.ViewModels;

namespace DataAccess.Abstarct
{
    public interface ITenantDal : IEntityRepository<Tenant>
    {
        List<TenantVM> GetTenantVMs(int apartmentComplexId,string? blockName =null, string? nameFilter = null, bool onlyHasDebt =false);
        TenantVM GetTenantVMById(int id);
    }
}

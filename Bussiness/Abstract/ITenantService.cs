using Core.Utilities;
using Entity;

namespace Bussiness.Abstract
{
    public interface ITenantService
    {
        Result Add(Tenant tenant);
        Result Delete(Tenant tenant);
        Result Update(Tenant tenant);
        Result DeleteById(int id);

        DataResult<List<Tenant>> GetList();
        DataResult<Tenant> GetById(int id);

    }
}

using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Entity;

namespace DataAccess.Abstarct
{
    public interface ITenantDal : IEntityRepository<Tenant>
    {
    }
}

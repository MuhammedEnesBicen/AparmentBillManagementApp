
using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;

namespace DataAccess.Concrete
{
    public class EfTenantDal : EfEntityRepositoryBase<Tenant,AppDbContext>,ITenantDal
    {
    }
}

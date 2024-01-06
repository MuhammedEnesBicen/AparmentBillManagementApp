using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;

namespace DataAccess.Concrete
{
    public class EfManagerDal : EfEntityRepositoryBase<Manager, AppDbContext>, IManagerDal
    {
    }
}

using Core.DataAccess;
using Core.Utilities;
using Entity;

namespace DataAccess.Abstract
{
    public interface IManagerDal : IEntityRepository<Manager>
    {
        public Result AddManagerWithApartmentComplex(Manager manager);
    }
}

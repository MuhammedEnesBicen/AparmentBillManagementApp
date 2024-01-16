using Core.DataAccess;
using Core.Utilities;
using Entity;

namespace DataAccess.Abstarct
{
    public interface IManagerDal : IEntityRepository<Manager>
    {
        public Result AddManagerWithApartmentComplex(Manager manager);
    }
}

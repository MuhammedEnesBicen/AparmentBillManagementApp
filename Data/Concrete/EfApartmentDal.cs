using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;

namespace DataAccess.Concrete
{
    public class EfApartmentDal: EfEntityRepositoryBase<Apartment,AppDbContext>,IApartmentDal
    {
    }
}

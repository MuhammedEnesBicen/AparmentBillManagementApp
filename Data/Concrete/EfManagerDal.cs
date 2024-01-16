using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;

namespace DataAccess.Concrete
{
    public class EfManagerDal : EfEntityRepositoryBase<Manager, AppDbContext>, IManagerDal
    {
        public Result AddManagerWithApartmentComplex(Manager manager)
        {
            using (var context = new AppDbContext())
            {
                var aComplex = context.ApartmentComplexes.Add(manager.ApartmentComplex);
                aComplex.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                manager.ApartmentComplexId = aComplex.Entity.Id;
                manager.ApartmentComplex.Id = aComplex.Entity.Id;
                context.Managers.Add(manager);
                context.SaveChanges();
                return new Result(true, "Manager added successfully");
            }
        }
    }
}

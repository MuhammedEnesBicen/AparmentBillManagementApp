using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class EfBillDal : EfEntityRepositoryBase<Bill, AppDbContext>, IBillDal
    {
        public List<Bill> GetListWithRelatedData(int page, Expression<Func<Bill, bool>> filter = null)
        {
            using (var context = new AppDbContext())
            {
                return filter == null
                    ? context.Set<Bill>().Include(b => b.Apartment).OrderByDescending(b => b.Id).Skip(page * 10).Take(10).ToList()
                    : context.Set<Bill>().Include(b => b.Apartment).Where(filter).OrderByDescending(b => b.Id).Skip(page * 10).Take(10).ToList();
            }
        }

        public Bill GetWithRelatedData(Expression<Func<Bill, bool>> filter)
        {
            using (var context = new AppDbContext())
            {
                return context.Set<Bill>().Include(b => b.Apartment).SingleOrDefault(filter);
            }
        }
    }
}
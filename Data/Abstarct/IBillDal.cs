using Core.DataAccess;
using Entity;
using System.Linq.Expressions;

namespace DataAccess.Abstarct
{
    public interface IBillDal : IEntityRepository<Bill>
    {
        public Bill GetWithRelatedData(Expression<Func<Bill, bool>> filter);
        public List<Bill> GetListWithRelatedData(int page, Expression<Func<Bill, bool>> filter = null);
    }
}

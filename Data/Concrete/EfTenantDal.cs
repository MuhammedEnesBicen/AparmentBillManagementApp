using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity;
using Entity.ViewModels;

namespace DataAccess.Concrete
{
    public class EfTenantDal : EfEntityRepositoryBase<Tenant, AppDbContext>, ITenantDal
    {
        public TenantVM GetTenantVMById(int id)
        {
            using (var context = new AppDbContext())
            {
                var result = from tenant in context.Tenants
                             join apartment in context.Apartments
                             on tenant.ApartmentId equals apartment.Id
                             join bill in context.Bills
                             on apartment.Id equals bill.ApartmentId into bills
                             where tenant.Id == id
                             select new TenantVM
                             {
                                 Tenant = tenant,
                                 BlockName = apartment.BlockName,
                                 FlatNumber = apartment.Number,
                                 DebtAmount = bills.Where(b => b.IsPayed == false).Sum(b => b.BillCost),
                                 ApartmentId = apartment.Id
                             };
                return result.FirstOrDefault();
            }
        }

        public List<TenantVM> GetTenantVMs(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false)
        {
            using (var context = new AppDbContext())
            {
                var result = from tenant in context.Tenants
                             join apartment in context.Apartments
                             on tenant.ApartmentId equals apartment.Id
                             join bill in context.Bills
                             on apartment.Id equals bill.ApartmentId into bills
                             where apartment.ApartmentComplexId == apartmentComplexId
                             select new TenantVM
                             {
                                 Tenant = tenant,
                                 BlockName = apartment.BlockName,
                                 FlatNumber = apartment.Number,
                                 DebtAmount = bills.Where(b => b.IsPayed == false).Sum(b => b.BillCost)
                             };
                if (blockName != null)
                    result = result.Where(t => t.BlockName == blockName);
                if (onlyHasDebt)
                    result = result.Where(t => t.DebtAmount > 0);
                if (String.IsNullOrEmpty(nameFilter) == false)
                    result = result.Where(t => (t.Tenant.Name + " " + t.Tenant.LastName).ToLower().Contains(nameFilter.ToLower()));

                return result.ToList();
            }
        }
    }
}

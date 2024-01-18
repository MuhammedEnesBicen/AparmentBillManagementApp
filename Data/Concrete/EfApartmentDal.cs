using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;
using Entity.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Concrete
{
    public class EfApartmentDal : EfEntityRepositoryBase<Apartment, AppDbContext>, IApartmentDal
    {
        public List<ApartmentVM> GetApartmentVMsByComplexId(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false)
        {
            using (var context = new AppDbContext())
            {
                var result = (from apartment in context.Apartments
                              join tenant in context.Tenants on apartment.Id equals tenant.ApartmentId into tenantsGroup
                              from tenant in tenantsGroup.DefaultIfEmpty()
                              join bill in context.Bills on apartment.Id equals bill.ApartmentId into billsGroup
                              where apartment.ApartmentComplexId == apartmentComplexId
                              select new ApartmentVM
                              {
                                  Apartment = apartment,
                                  TenantId = (tenant == null) ? -1 : tenant.Id,
                                  TenantName = (tenant == null) ? String.Empty : tenant.Name + " " + tenant.LastName,
                                  DebtAmount = (billsGroup.IsNullOrEmpty()) ? 0 : billsGroup.Where(x => x.IsPayed == false).Sum(x => x.BillCost)
                              }).AsEnumerable();
                if (String.IsNullOrEmpty(blockName) == false)
                    result = result.Where(x => x.Apartment.BlockName == blockName);
                if (onlyHasDebt)
                    result = result.Where(a => a.DebtAmount > 0);
                if (String.IsNullOrEmpty(nameFilter) == false)
                    result = result.Where(t => t.TenantName.ToLower().Contains(nameFilter.ToLower()));
                return result.ToList();
            }
        }
    }
}

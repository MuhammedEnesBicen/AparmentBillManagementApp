using Core.DataAccess.EntityFramework;
using DataAccess.Abstarct;
using Entity;
using Entity.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Concrete
{
    public class EfApartmentDal : EfEntityRepositoryBase<Apartment, AppDbContext>, IApartmentDal
    {
        public List<ApartmentVM> GetApartmentVMs(string? blockName=null)
        {
            using (var context = new AppDbContext())
            {
                var result = from apartment in context.Apartments
                              join tenant in context.Tenants on apartment.Id equals tenant.ApartmentId into tenantsGroup
                             from tenant in tenantsGroup.DefaultIfEmpty()
                             join bill in context.Bills on apartment.Id equals bill.ApartmentId into billsGroup
                             //from bills in billsGroup.DefaultIfEmpty()                             

                             select new ApartmentVM
                             {
                                 Apartment = apartment,
                                 TenantName = (tenant == null)?String.Empty:tenant.Name + " " + tenant.LastName,
                                 DebtAmount = (billsGroup.IsNullOrEmpty())?0:billsGroup.Where(x => x.IsPayed == false).Sum(x => x.BillCost)
                             };
                if(blockName.IsNullOrEmpty()==false) return result.Where(x => x.Apartment.BlockName == blockName).ToList();
                return result.ToList();
            }
        }
    }
}

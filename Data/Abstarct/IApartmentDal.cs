using Core.DataAccess;
using Entity;

namespace DataAccess.Abstarct
{
    public interface IApartmentDal: IEntityRepository<Apartment>
    {
        public List<Entity.ViewModels.ApartmentVM> GetApartmentVMsByComplexId(int apartmentComplexId,string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);
    }
}

using Core.DataAccess;
using Entity;

namespace DataAccess.Abstarct
{
    public interface IApartmentDal: IEntityRepository<Apartment>
    {
        public List<Entity.ViewModels.ApartmentVM> GetApartmentVMs(string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);
    }
}

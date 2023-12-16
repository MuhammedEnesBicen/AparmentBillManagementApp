using Core.Utilities;
using Entity;
using Entity.ViewModels;
namespace Bussiness.Abstract
{
    public interface IApartmentService
    {
        Result Add(Apartment apartment);
        Result Delete(Apartment apartment);
        Result Update(Apartment apartment);

        DataResult<Apartment> GetById(int apartmentId);
        DataResult<List<Apartment>> GetList();
        DataResult<List<ApartmentVM>> GetApartmentVMs(string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);

        Result DeleteById(int id);
    }
}

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
        DataResult<List<ApartmentVM>> GetApartmentVMs();
        DataResult<List<ApartmentVM>> GetApartmentVMsByBlock(String blockName);

        Result DeleteById(int id);
    }
}

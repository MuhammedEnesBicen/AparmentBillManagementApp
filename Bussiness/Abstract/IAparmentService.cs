using Core.Utilities;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;
namespace Bussiness.Abstract
{
    public interface IApartmentService
    {
        Result Add(ApartmentDTO apartmentDTO);
        Result Delete(Apartment apartment);
        Result Update(ApartmentDTO apartmentDTO);

        DataResult<Apartment> GetById(int apartmentId);
        DataResult<List<Apartment>> GetList();
        DataResult<List<ApartmentVM>> GetApartmentVMsByComplexId(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false);

        Result DeleteById(int id);
    }
}

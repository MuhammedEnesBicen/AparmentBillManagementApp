using Core.Utilities;
using Entity;
using Entity.DTOs;

namespace Bussiness.Abstract
{
    public interface IBillService
    {
        Result Add(BillDTO billDTO);
        Result Delete(Bill bill);
        Result Update(BillDTO billDTO);

        Result DeleteById(int id);

        DataResult<Bill> GetById(int id);
        DataResult<List<Bill>> GetList();

        DataResult<List<Bill>> GetListByApartmentId(int apartmentId, int billPage);

        DataResult<List<Bill>> GetListWithRelatedData(int apartmentComplexId, int page, int? apartmentId = null);
        DataResult<Bill> GetWithRelatedData(int billId);
    }
}

using Core.Utilities;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IApartmentService
    {
        Result Add(Apartment apartment);
        Result Delete(Apartment apartment);
        Result Update(Apartment apartment);

        DataResult<Apartment> GetById(int apartmentId);
        DataResult<List<Apartment>> GetByBlockList(String blockName);
        DataResult<List<Apartment>> GetList();

        Result DeleteById(int id);
    }
}

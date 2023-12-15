using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.ViewModels;

namespace Bussiness.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IApartmentDal apartmentDal;

        public ApartmentManager(IApartmentDal dal)
        {
            apartmentDal = dal;
        }
        public Result Add(Apartment apartment)
        {
            apartmentDal.Add(apartment);
            return new Result(true,"Successfully Added");
        }

        public Result Delete(Apartment apartment)
        {
            apartmentDal.Delete(apartment);
            return new Result(true,"Apartment deleted succesfully");
        }

        public Result DeleteById(int id)
        {
            var result = GetById(id);
            if (result.Success = true)
            {
                return Delete(result.Data);

            }
            return new Result(false, "Apartment couldnt deleted");
        }

        public DataResult<List<ApartmentVM>> GetApartmentVMs()
        {
            var result = apartmentDal.GetApartmentVMs();
            return new DataResult<List<ApartmentVM>>(true, "Succesfully Listed", result);
        }

        public DataResult<List<ApartmentVM>> GetApartmentVMsByBlock(String blockName)
        {
            return new DataResult<List<ApartmentVM>>(true, "Succesfully Listed", apartmentDal.GetApartmentVMs(blockName).ToList());

        }

        public DataResult<Apartment> GetById(int apartmentId)
        {
            var result = apartmentDal.Get(a => a.Id == apartmentId);
            if (result is not null)
            return new DataResult<Apartment>(true, "Succesfully Listed", result);
            return new DataResult<Apartment>(false, "Item not found ", result);

        }

        public DataResult<List<Apartment>> GetList()
        {
            return new DataResult<List<Apartment>>(true, "Succesfully Listed", apartmentDal.GetList().ToList());
        }

        public Result Update(Apartment apartment)
        {
            var item = GetById(apartment.Id).Data;
            if (item == null)
                return new Result(false, "There is no Apartment with this id");
            item.BlockName = apartment.BlockName;
            item.Number = apartment.Number;
            item.Floor = apartment.Floor;
            item.Type = apartment.Type;
            apartmentDal.Update(item);
            return new Result(true, "Apartment updated successfully");
        }
    }
}

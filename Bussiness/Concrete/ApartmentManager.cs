using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using Entity;
using Entity.DTOs;
using Entity.ViewModels;

namespace Bussiness.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IApartmentDal apartmentDal;
        private readonly IMapper mapper;

        public ApartmentManager(IApartmentDal dal, IMapper mapper)
        {
            apartmentDal = dal;
            this.mapper = mapper;
        }
        public Result Add(ApartmentDTO apartmentDTO)
        {
            Apartment apartment = mapper.Map<Apartment>(apartmentDTO);
            apartmentDal.Add(apartment);
            return new Result(true, "Successfully Added");
        }

        public Result Delete(Apartment apartment)
        {
            apartmentDal.Delete(apartment);
            return new Result(true, "Apartment deleted succesfully");
        }

        public Result DeleteById(int id)
        {
            var result = GetById(id);
            if (result.Success)
            {
                return Delete(result.Data);

            }
            return new Result(false, result.Message);
        }

        public DataResult<List<ApartmentVM>> GetApartmentVMsByComplexId(int apartmentComplexId, string? blockName = null, string? nameFilter = null, bool onlyHasDebt = false)
        {
            var result = apartmentDal.GetApartmentVMsByComplexId(apartmentComplexId, blockName, nameFilter, onlyHasDebt);
            return new DataResult<List<ApartmentVM>>(true, "Succesfully Listed", result);
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

        public Result Update(ApartmentDTO apartmentDTO)
        {
            var item = GetById(apartmentDTO.Id).Data;
            if (item == null)
                return new Result(false, "There is no Apartment with this id");
            mapper.Map(apartmentDTO, item);
            apartmentDal.Update(item);
            return new Result(true, "Apartment updated successfully");
        }
    }
}

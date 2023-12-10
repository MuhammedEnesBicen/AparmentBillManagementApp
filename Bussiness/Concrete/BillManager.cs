﻿using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;

namespace Bussiness.Concrete
{
    public class BillManager : IBillService
    {
        private readonly IBillDal billDal;
        private readonly IMapper mapper;

        public BillManager(IBillDal billDal, IMapper mapper)
        {
            this.billDal = billDal;
            this.mapper = mapper;
        }

        public Result Add(BillDTO billDTO)
        {
            var result = mapper.Map<BillDTO, Bill>(billDTO);
            billDal.Add(result);
            return new Result(true, "Bill Added Successfully");
        }

        public Result Delete(Bill bill)
        {
            billDal.Delete(bill);
            return new Result(true, "Bill Deleted Successfully");

        }

        public Result DeleteById(int id)
        {
            var result = GetById(id);
            if(result.Success==false)
            {
                return new Result(false, "Bill not found in database.");
            }
            return Delete(result.Data);

        }

        public DataResult<Bill> GetById(int id)
        {
           var bill = billDal.Get(b=> b.Id==id);
            if(bill is null)
            {
                return new DataResult<Bill>(false, "Bill not found in database.", bill);
            }
            return new DataResult<Bill>(true, "Bill successfully retrieved", bill);
        }

        public DataResult<List<Bill>> GetList()
        {
            var result =  billDal.GetList().ToList();
            return new DataResult<List<Bill>>(true, "Bills listed successfully", result);
        }

        public DataResult<List<Bill>> GetListByApartmentId(int apartmentId)
        {
            var result = billDal.GetList(b=> b.ApartmentId == apartmentId).ToList();
            return new DataResult<List<Bill>>(true, "Bills of apartment listed successfully", result);
        }

        public DataResult<List<Bill>> GetListWithRelatedData(int? apartmentId =null)
        {
            var result =
            (apartmentId == null)?
            billDal.GetListWithRelatedData():billDal.GetListWithRelatedData(b=> b.ApartmentId==apartmentId);

            return new DataResult<List<Bill>>(true,"Bills listed successfully",result);
        }


        public DataResult<Bill> GetWithRelatedData(int billId)
        {
            var result = billDal.GetWithRelatedData(b => b.Id == billId);
            return new DataResult<Bill>(true, "Bill retrieved successfully", result);
        }


        public Result Update(BillDTO billDTO)
        {
            var billFromDb = GetById(billDTO.Id);
            if(billFromDb.Success is false)
            {
                return new Result(false, "Bill not found in database.");   
            }
            mapper.Map<BillDTO,Bill>(billDTO, billFromDb.Data);
            billDal.Update(billFromDb.Data);
            return new Result(true, "Bill Updated Successfully");
        }
    }
}
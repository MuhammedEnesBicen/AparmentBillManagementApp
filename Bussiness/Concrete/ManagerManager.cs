using AutoMapper;
using Bussiness.Abstract;
using Core.Utilities;
using DataAccess.Abstarct;
using Entity;
using Entity.DTOs;

namespace Bussiness.Concrete
{
    public class ManagerManager : IManagerService
    {
        private readonly IManagerDal managerDal;
        private readonly IMapper mapper;

        public ManagerManager(IManagerDal managerDal, IMapper mapper)
        {
            this.managerDal = managerDal;
            this.mapper = mapper;
        }

        public Result Add(Manager manager)
        {
            var result = GetByMail(manager.Mail);
            if (result.Data != null)
                return new Result(false, "Manager with this mail already exists");
            managerDal.Add(manager);
            return new Result(true, "Manager added successfully");
        }

        public Result AddManagerWithApartmentComplex(Manager manager)
        {
            var result = GetByMail(manager.Mail);
            if (result.Data != null)
                return new Result(false, "Manager with this mail already exists");
            return managerDal.AddManagerWithApartmentComplex(manager);
        }

        public Result Delete(Manager manager)
        {
            managerDal.Delete(manager);
            return new Result(true, "Manager deleted successfully");
        }

        public Result DeleteById(int managerId)
        {
            var manager = GetById(managerId);
            if (manager.Data == null)
                return new Result(false, "Manager not found");
            managerDal.Delete(manager.Data);
            return new Result(true, "Manager deleted successfully");
        }

        public DataResult<Manager> GetById(int id)
        {
            var manager = managerDal.Get(m => m.Id == id);
            if (manager == null)
                return new DataResult<Manager>(false, "Manager not found", null);
            return new DataResult<Manager>(true, "Manager fetched successfully", manager);
        }

        public DataResult<Manager> GetByMail(string mail)
        {
            var manager = managerDal.Get(m => m.Mail == mail);
            if (manager == null)
                return new DataResult<Manager>(false, "Manager not found", null);
            return new DataResult<Manager>(true, "Manager fetched successfully", manager);
        }

        public DataResult<List<Manager>> GetList()
        {
            var result = managerDal.GetList().ToList();
            return new DataResult<List<Manager>>(true, "Managers fetched successfully", result);
        }

        public DataResult<Manager> Login(LoginDTO loginDTO)
        {
            var manager = GetByMail(loginDTO.Mail);
            if (manager.Data == null)
                return new DataResult<Manager>(false, "There is no manager with this mail.", null);
            if (manager.Data.Password != loginDTO.Password)
                return new DataResult<Manager>(false, "Password is wrong.", null);
            return new DataResult<Manager>(true, "Login successful", manager.Data);

        }

        public Result Update(Manager manager)
        {
            var managerToUpdate = GetById(manager.Id);
            if (managerToUpdate.Data == null)
                return new Result(false, "Manager not found");

            managerToUpdate.Data = mapper.Map<Manager>(manager);
            managerDal.Update(managerToUpdate.Data);
            return new Result(true, "Manager updated successfully");
        }
    }
}

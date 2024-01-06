using Core.Utilities;
using Entity;
using Entity.DTOs;

namespace Bussiness.Abstract
{
    public interface IManagerService
    {
        Result Add(Manager manager);
        Result Update(Manager manager);
        Result Delete(Manager manager);
        Result DeleteById(int managerId);
        DataResult<Manager> GetById(int id);
        DataResult<Manager> GetByMail(string mail);
        DataResult<Manager> Login(LoginDTO loginDTO);

        DataResult<List<Manager>> GetList();
    }
}

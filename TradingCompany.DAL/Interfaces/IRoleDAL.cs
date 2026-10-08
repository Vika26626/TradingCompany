using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface IRoleDAL
    {
        void Add(Role role);

        Role? GetById(int id);

        List<Role> GetAll();

        void Update(Role role);

        void Delete(int id);
    }
}
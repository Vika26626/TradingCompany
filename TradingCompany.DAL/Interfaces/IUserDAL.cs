using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface IUserDAL
    {
        void Add(User user);

        User? GetById(int id);

        List<User> GetAll();

        void Update(User user);

        void Delete(int id);
    }
}
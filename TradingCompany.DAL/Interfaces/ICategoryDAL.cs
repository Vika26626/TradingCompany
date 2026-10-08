using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface ICategoryDAL
    {
        void Add(Category category);

        Category? GetById(int id);

        List<Category> GetAll();

        void Update(Category category);

        void Delete(int id);
    }
}
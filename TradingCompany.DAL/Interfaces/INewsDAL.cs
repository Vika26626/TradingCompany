using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface INewsDAL
    {
        void Add(News news);

        News? GetById(int id);

        List<News> GetAll();

        void Update(News news);

        void Delete(int id);
    }
}
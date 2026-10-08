using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface IProductDAL
    {
        void Add(Product product);

        Product? GetById(int id);

        List<Product> GetAll();

        void Update(Product product);

        void Delete(int id);
    }
}
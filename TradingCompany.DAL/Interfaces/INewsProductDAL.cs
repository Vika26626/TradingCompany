using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.Interfaces
{
    public interface INewsProductDAL
    {
        void Add(NewsProduct newsProduct);

        NewsProduct? GetById(int newsId, int productId);

        List<NewsProduct> GetAll();

        void Update(NewsProduct newsProduct, int oldNewsId, int oldProductId);

        void Delete(int newsId, int productId);
    }
}
using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class NewsProductDALTests
    {
        private NewsProductDAL _newsProductDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _newsProductDal = new NewsProductDAL(ConnectionString);
        }

        [Test]
        public void AddNewsProduct_ValidLink_LinkIsAdded()
        {
            var pair = FindFreePair();

            // CREATE
            var newsProduct = new NewsProduct
            {
                NewsId = pair.newsId,
                ProductId = pair.productId
            };

            _newsProductDal.Add(newsProduct);

            var foundNewsProduct =
                _newsProductDal.GetById(
                    newsProduct.NewsId,
                    newsProduct.ProductId);

            Assert.That(foundNewsProduct, Is.Not.Null);
            Assert.That(
                foundNewsProduct!.NewsId,
                Is.EqualTo(newsProduct.NewsId));

            Assert.That(
                foundNewsProduct.ProductId,
                Is.EqualTo(newsProduct.ProductId));

            // Очищення
            _newsProductDal.Delete(
                newsProduct.NewsId,
                newsProduct.ProductId);
        }

        [Test]
        public void GetById_ExistingLink_ReturnsLink()
        {
            var pair = FindFreePair();

            // Створюємо тестовий зв'язок
            var newsProduct = new NewsProduct
            {
                NewsId = pair.newsId,
                ProductId = pair.productId
            };

            _newsProductDal.Add(newsProduct);

            // READ
            var foundNewsProduct =
                _newsProductDal.GetById(
                    newsProduct.NewsId,
                    newsProduct.ProductId);

            Assert.That(foundNewsProduct, Is.Not.Null);
            Assert.That(
                foundNewsProduct!.NewsId,
                Is.EqualTo(newsProduct.NewsId));

            Assert.That(
                foundNewsProduct.ProductId,
                Is.EqualTo(newsProduct.ProductId));

            // Очищення
            _newsProductDal.Delete(
                newsProduct.NewsId,
                newsProduct.ProductId);
        }

        [Test]
        public void UpdateLink_ExistingLink_LinkIsUpdated()
        {
            var pair = FindFreePair();

            // Створюємо тестовий зв'язок
            var newsProduct = new NewsProduct
            {
                NewsId = pair.newsId,
                ProductId = pair.productId
            };

            _newsProductDal.Add(newsProduct);

            int oldNewsId = newsProduct.NewsId;
            int oldProductId = newsProduct.ProductId;

            // Знаходимо іншу вільну пару для UPDATE
            var newPair = FindFreePair();

            newsProduct.NewsId = newPair.newsId;
            newsProduct.ProductId = newPair.productId;

            // UPDATE
            _newsProductDal.Update(
                newsProduct,
                oldNewsId,
                oldProductId);

            var updatedNewsProduct =
                _newsProductDal.GetById(
                    newsProduct.NewsId,
                    newsProduct.ProductId);

            Assert.That(updatedNewsProduct, Is.Not.Null);
            Assert.That(
                updatedNewsProduct!.NewsId,
                Is.EqualTo(newPair.newsId));

            Assert.That(
                updatedNewsProduct.ProductId,
                Is.EqualTo(newPair.productId));

            // Очищення
            _newsProductDal.Delete(
                newsProduct.NewsId,
                newsProduct.ProductId);
        }

        [Test]
        public void DeleteLink_ExistingLink_LinkIsDeleted()
        {
            var pair = FindFreePair();

            // Створюємо тестовий зв'язок
            var newsProduct = new NewsProduct
            {
                NewsId = pair.newsId,
                ProductId = pair.productId
            };

            _newsProductDal.Add(newsProduct);

            // DELETE
            _newsProductDal.Delete(
                newsProduct.NewsId,
                newsProduct.ProductId);

            var deletedNewsProduct =
                _newsProductDal.GetById(
                    newsProduct.NewsId,
                    newsProduct.ProductId);

            Assert.That(deletedNewsProduct, Is.Null);
        }

        private (int newsId, int productId) FindFreePair()
        {
            var existingLinks = _newsProductDal.GetAll();

            for (int newsId = 1; newsId <= 20; newsId++)
            {
                for (int productId = 1; productId <= 20; productId++)
                {
                    bool exists = existingLinks.Exists(
                        x => x.NewsId == newsId &&
                             x.ProductId == productId);

                    if (!exists)
                    {
                        return (newsId, productId);
                    }
                }
            }

            throw new Exception(
                "Не знайдено вільної пари NewsId + ProductId.");
        }
    }
}
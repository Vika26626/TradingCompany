using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class NewsDALTests
    {
        private NewsDAL _newsDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _newsDal = new NewsDAL(ConnectionString);
        }

        [Test]
        public void AddNews_ValidNews_NewsIsAdded()
        {
            // CREATE
            var news = new News
            {
                Title = "Test News NUnit",
                Content = "Test news content.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUserId = 1
            };

            _newsDal.Add(news);

            var newsList = _newsDal.GetAll();
            var addedNews = newsList[^1];

            Assert.That(addedNews, Is.Not.Null);
            Assert.That(
                addedNews.Title,
                Is.EqualTo("Test News NUnit"));

            // Очищення
            _newsDal.Delete(addedNews.NewsId);
        }

        [Test]
        public void GetById_ExistingNews_ReturnsNews()
        {
            // Створюємо тестову новину
            var news = new News
            {
                Title = "Test News GetById",
                Content = "Test news content.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUserId = 1
            };

            _newsDal.Add(news);

            var newsList = _newsDal.GetAll();
            var addedNews = newsList[^1];

            // READ
            var foundNews =
                _newsDal.GetById(addedNews.NewsId);

            Assert.That(foundNews, Is.Not.Null);
            Assert.That(
                foundNews!.NewsId,
                Is.EqualTo(addedNews.NewsId));

            Assert.That(
                foundNews.Title,
                Is.EqualTo("Test News GetById"));

            // Очищення
            _newsDal.Delete(addedNews.NewsId);
        }

        [Test]
        public void UpdateNews_ExistingNews_NewsIsUpdated()
        {
            // Створюємо тестову новину
            var news = new News
            {
                Title = "Test News Before Update",
                Content = "Old content.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUserId = 1
            };

            _newsDal.Add(news);

            var newsList = _newsDal.GetAll();
            var addedNews = newsList[^1];

            // UPDATE
            addedNews.Title = "Test News After Update";
            addedNews.Content = "Updated content.";
            addedNews.IsActive = false;

            _newsDal.Update(addedNews);

            var updatedNews =
                _newsDal.GetById(addedNews.NewsId);

            Assert.That(updatedNews, Is.Not.Null);
            Assert.That(
                updatedNews!.Title,
                Is.EqualTo("Test News After Update"));

            Assert.That(
                updatedNews.Content,
                Is.EqualTo("Updated content."));

            Assert.That(
                updatedNews.IsActive,
                Is.False);

            // Очищення
            _newsDal.Delete(addedNews.NewsId);
        }

        [Test]
        public void DeleteNews_ExistingNews_NewsIsDeleted()
        {
            // Створюємо тестову новину
            var news = new News
            {
                Title = "Test News To Delete",
                Content = "Test news content.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUserId = 1
            };

            _newsDal.Add(news);

            var newsList = _newsDal.GetAll();
            var addedNews = newsList[^1];

            // DELETE
            _newsDal.Delete(addedNews.NewsId);

            var deletedNews =
                _newsDal.GetById(addedNews.NewsId);

            Assert.That(deletedNews, Is.Null);
        }
    }
}
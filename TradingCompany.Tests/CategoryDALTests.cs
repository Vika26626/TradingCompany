using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class CategoryDALTests
    {
        private CategoryDAL _categoryDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _categoryDal = new CategoryDAL(ConnectionString);
        }

        [Test]
        public void AddCategory_ValidCategory_CategoryIsAdded()
        {
            // CREATE
            var category = new Category
            {
                CategoryName = "Test Category NUnit"
            };

            _categoryDal.Add(category);

            var categories = _categoryDal.GetAll();
            var addedCategory = categories[^1];

            Assert.That(addedCategory, Is.Not.Null);
            Assert.That(addedCategory.CategoryName, Is.EqualTo("Test Category NUnit"));

            // Очищення
            _categoryDal.Delete(addedCategory.CategoryId);
        }

        [Test]
        public void GetById_ExistingCategory_ReturnsCategory()
        {
            // Створюємо тестовий запис
            var category = new Category
            {
                CategoryName = "Test Category GetById"
            };

            _categoryDal.Add(category);

            var categories = _categoryDal.GetAll();
            var addedCategory = categories[^1];

            // READ
            var foundCategory = _categoryDal.GetById(addedCategory.CategoryId);

            Assert.That(foundCategory, Is.Not.Null);
            Assert.That(foundCategory!.CategoryId, Is.EqualTo(addedCategory.CategoryId));
            Assert.That(foundCategory.CategoryName, Is.EqualTo("Test Category GetById"));

            // Очищення
            _categoryDal.Delete(addedCategory.CategoryId);
        }

        [Test]
        public void UpdateCategory_ExistingCategory_CategoryIsUpdated()
        {
            // Створюємо тестовий запис
            var category = new Category
            {
                CategoryName = "Test Category Before Update"
            };

            _categoryDal.Add(category);

            var categories = _categoryDal.GetAll();
            var addedCategory = categories[^1];

            // UPDATE
            addedCategory.CategoryName = "Test Category After Update";

            _categoryDal.Update(addedCategory);

            var updatedCategory =
                _categoryDal.GetById(addedCategory.CategoryId);

            Assert.That(updatedCategory, Is.Not.Null);
            Assert.That(
                updatedCategory!.CategoryName,
                Is.EqualTo("Test Category After Update"));

            // Очищення
            _categoryDal.Delete(addedCategory.CategoryId);
        }

        [Test]
        public void DeleteCategory_ExistingCategory_CategoryIsDeleted()
        {
            // Створюємо тестовий запис
            var category = new Category
            {
                CategoryName = "Test Category To Delete"
            };

            _categoryDal.Add(category);

            var categories = _categoryDal.GetAll();
            var addedCategory = categories[^1];

            // DELETE
            _categoryDal.Delete(addedCategory.CategoryId);

            var deletedCategory =
                _categoryDal.GetById(addedCategory.CategoryId);

            Assert.That(deletedCategory, Is.Null);
        }
    }
}
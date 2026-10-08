using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class ProductDALTests
    {
        private ProductDAL _productDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _productDal = new ProductDAL(ConnectionString);
        }

        [Test]
        public void AddProduct_ValidProduct_ProductIsAdded()
        {
            // CREATE
            var product = new Product
            {
                ProductName = "Test Product NUnit",
                Price = 100.50m,
                Quantity = 10,
                CategoryId = 1
            };

            _productDal.Add(product);

            var products = _productDal.GetAll();
            var addedProduct = products[^1];

            Assert.That(addedProduct, Is.Not.Null);
            Assert.That(
                addedProduct.ProductName,
                Is.EqualTo("Test Product NUnit"));

            // Очищення
            _productDal.Delete(addedProduct.ProductId);
        }

        [Test]
        public void GetById_ExistingProduct_ReturnsProduct()
        {
            // Створюємо тестовий товар
            var product = new Product
            {
                ProductName = "Test Product GetById",
                Price = 150.00m,
                Quantity = 5,
                CategoryId = 1
            };

            _productDal.Add(product);

            var products = _productDal.GetAll();
            var addedProduct = products[^1];

            // READ
            var foundProduct =
                _productDal.GetById(addedProduct.ProductId);

            Assert.That(foundProduct, Is.Not.Null);
            Assert.That(
                foundProduct!.ProductId,
                Is.EqualTo(addedProduct.ProductId));

            Assert.That(
                foundProduct.ProductName,
                Is.EqualTo("Test Product GetById"));

            // Очищення
            _productDal.Delete(addedProduct.ProductId);
        }

        [Test]
        public void UpdateProduct_ExistingProduct_ProductIsUpdated()
        {
            // Створюємо тестовий товар
            var product = new Product
            {
                ProductName = "Test Product Before Update",
                Price = 200.00m,
                Quantity = 10,
                CategoryId = 1
            };

            _productDal.Add(product);

            var products = _productDal.GetAll();
            var addedProduct = products[^1];

            // UPDATE
            addedProduct.ProductName = "Test Product After Update";
            addedProduct.Price = 250.00m;
            addedProduct.Quantity = 20;

            _productDal.Update(addedProduct);

            var updatedProduct =
                _productDal.GetById(addedProduct.ProductId);

            Assert.That(updatedProduct, Is.Not.Null);
            Assert.That(
                updatedProduct!.ProductName,
                Is.EqualTo("Test Product After Update"));

            Assert.That(
                updatedProduct.Price,
                Is.EqualTo(250.00m));

            Assert.That(
                updatedProduct.Quantity,
                Is.EqualTo(20));

            // Очищення
            _productDal.Delete(addedProduct.ProductId);
        }

        [Test]
        public void DeleteProduct_ExistingProduct_ProductIsDeleted()
        {
            // Створюємо тестовий товар
            var product = new Product
            {
                ProductName = "Test Product To Delete",
                Price = 50.00m,
                Quantity = 5,
                CategoryId = 1
            };

            _productDal.Add(product);

            var products = _productDal.GetAll();
            var addedProduct = products[^1];

            // DELETE
            _productDal.Delete(addedProduct.ProductId);

            var deletedProduct =
                _productDal.GetById(addedProduct.ProductId);

            Assert.That(deletedProduct, Is.Null);
        }
    }
}

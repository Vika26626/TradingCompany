using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class RoleDALTests
    {
        private RoleDAL _roleDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _roleDal = new RoleDAL(ConnectionString);
        }

        [Test]
        public void AddRole_ValidRole_RoleIsAdded()
        {
            // CREATE
            var role = new Role
            {
                RoleName = "Test Role NUnit"
            };

            _roleDal.Add(role);

            var roles = _roleDal.GetAll();
            var addedRole = roles[^1];

            Assert.That(addedRole, Is.Not.Null);
            Assert.That(addedRole.RoleName, Is.EqualTo("Test Role NUnit"));

            // Очищення тестових даних
            _roleDal.Delete(addedRole.RoleId);
        }

        [Test]
        public void GetById_ExistingRole_ReturnsRole()
        {
            // Спочатку створюємо тестовий запис
            var role = new Role
            {
                RoleName = "Test Role GetById"
            };

            _roleDal.Add(role);

            var roles = _roleDal.GetAll();
            var addedRole = roles[^1];

            // READ
            var foundRole = _roleDal.GetById(addedRole.RoleId);

            Assert.That(foundRole, Is.Not.Null);
            Assert.That(foundRole!.RoleId, Is.EqualTo(addedRole.RoleId));
            Assert.That(foundRole.RoleName, Is.EqualTo("Test Role GetById"));

            // Очищення
            _roleDal.Delete(addedRole.RoleId);
        }

        [Test]
        public void UpdateRole_ExistingRole_RoleIsUpdated()
        {
            // Створюємо тестовий запис
            var role = new Role
            {
                RoleName = "Test Role Before Update"
            };

            _roleDal.Add(role);

            var roles = _roleDal.GetAll();
            var addedRole = roles[^1];

            // UPDATE
            addedRole.RoleName = "Test Role After Update";

            _roleDal.Update(addedRole);

            var updatedRole = _roleDal.GetById(addedRole.RoleId);

            Assert.That(updatedRole, Is.Not.Null);
            Assert.That(updatedRole!.RoleName, Is.EqualTo("Test Role After Update"));

            // Очищення
            _roleDal.Delete(addedRole.RoleId);
        }

        [Test]
        public void DeleteRole_ExistingRole_RoleIsDeleted()
        {
            // Створюємо тестовий запис
            var role = new Role
            {
                RoleName = "Test Role To Delete"
            };

            _roleDal.Add(role);

            var roles = _roleDal.GetAll();
            var addedRole = roles[^1];

            // DELETE
            _roleDal.Delete(addedRole.RoleId);

            var deletedRole = _roleDal.GetById(addedRole.RoleId);

            Assert.That(deletedRole, Is.Null);
        }
    }
}
using NUnit.Framework;
using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class UserDALTests
    {
        private UserDAL _userDal = null!;

        private const string ConnectionString =
            "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

        [SetUp]
        public void SetUp()
        {
            _userDal = new UserDAL(ConnectionString);
        }

        [Test]
        public void AddUser_ValidUser_UserIsAdded()
        {
            // CREATE
            var user = new User
            {
                Login = "test_user_nunit",
                Password = "12345",
                FirstName = "Test",
                LastName = "User",
                IsActive = true,
                RoleId = 19
            };

            _userDal.Add(user);

            var users = _userDal.GetAll();
            var addedUser = users[^1];

            Assert.That(addedUser, Is.Not.Null);
            Assert.That(addedUser.Login, Is.EqualTo("test_user_nunit"));

            // Очищення
            _userDal.Delete(addedUser.UserId);
        }

        [Test]
        public void GetById_ExistingUser_ReturnsUser()
        {
            // Створюємо тестового користувача
            var user = new User
            {
                Login = "test_user_getbyid",
                Password = "12345",
                FirstName = "Test",
                LastName = "User",
                IsActive = true,
                RoleId = 19
            };

            _userDal.Add(user);

            var users = _userDal.GetAll();
            var addedUser = users[^1];

            // READ
            var foundUser = _userDal.GetById(addedUser.UserId);

            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser!.UserId, Is.EqualTo(addedUser.UserId));
            Assert.That(foundUser.Login, Is.EqualTo("test_user_getbyid"));

            // Очищення
            _userDal.Delete(addedUser.UserId);
        }

        [Test]
        public void UpdateUser_ExistingUser_UserIsUpdated()
        {
            // Створюємо тестового користувача
            var user = new User
            {
                Login = "test_user_before",
                Password = "12345",
                FirstName = "Test",
                LastName = "User",
                IsActive = true,
                RoleId = 19
            };

            _userDal.Add(user);

            var users = _userDal.GetAll();
            var addedUser = users[^1];

            // UPDATE
            addedUser.Login = "test_user_after";
            addedUser.FirstName = "Updated";

            _userDal.Update(addedUser);

            var updatedUser = _userDal.GetById(addedUser.UserId);

            Assert.That(updatedUser, Is.Not.Null);
            Assert.That(updatedUser!.Login, Is.EqualTo("test_user_after"));
            Assert.That(updatedUser.FirstName, Is.EqualTo("Updated"));

            // Очищення
            _userDal.Delete(addedUser.UserId);
        }

        [Test]
        public void DeleteUser_ExistingUser_UserIsDeleted()
        {
            // Створюємо тестового користувача
            var user = new User
            {
                Login = "test_user_delete",
                Password = "12345",
                FirstName = "Test",
                LastName = "User",
                IsActive = true,
                RoleId = 19
            };

            _userDal.Add(user);

            var users = _userDal.GetAll();
            var addedUser = users[^1];

            // DELETE
            _userDal.Delete(addedUser.UserId);

            var deletedUser = _userDal.GetById(addedUser.UserId);

            Assert.That(deletedUser, Is.Null);
        }
    }
}
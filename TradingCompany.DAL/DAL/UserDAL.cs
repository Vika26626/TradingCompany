using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly string _connectionString;

        public UserDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(User user)
        {
            const string sql =
                "INSERT INTO Users (Login, [Password], FirstName, LastName, IsActive, RoleId) " +
                "VALUES (@Login, @Password, @FirstName, @LastName, @IsActive, @RoleId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Login", user.Login);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@IsActive", user.IsActive);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public User? GetById(int id)
        {
            const string sql =
                "SELECT UserId, Login, [Password], FirstName, LastName, IsActive, RoleId " +
                "FROM Users WHERE UserId = @UserId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    Login = reader.GetString(1),
                    Password = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4),
                    IsActive = reader.GetBoolean(5),
                    RoleId = reader.GetInt32(6)
                };
            }

            return null;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            const string sql =
                "SELECT UserId, Login, [Password], FirstName, LastName, IsActive, RoleId " +
                "FROM Users ORDER BY UserId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32(0),
                    Login = reader.GetString(1),
                    Password = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4),
                    IsActive = reader.GetBoolean(5),
                    RoleId = reader.GetInt32(6)
                });
            }

            return users;
        }

        public void Update(User user)
        {
            const string sql =
                "UPDATE Users SET " +
                "Login = @Login, " +
                "[Password] = @Password, " +
                "FirstName = @FirstName, " +
                "LastName = @LastName, " +
                "IsActive = @IsActive, " +
                "RoleId = @RoleId " +
                "WHERE UserId = @UserId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@Login", user.Login);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@IsActive", user.IsActive);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql =
                "DELETE FROM Users WHERE UserId = @UserId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
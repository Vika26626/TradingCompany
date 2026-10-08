using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly string _connectionString;

        public CategoryDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Category category)
        {
            const string sql =
                "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public Category? GetById(int id)
        {
            const string sql =
                "SELECT CategoryId, CategoryName FROM Categories WHERE CategoryId = @CategoryId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Category
                {
                    CategoryId = reader.GetInt32(0),
                    CategoryName = reader.GetString(1)
                };
            }

            return null;
        }

        public List<Category> GetAll()
        {
            var categories = new List<Category>();

            const string sql =
                "SELECT CategoryId, CategoryName FROM Categories ORDER BY CategoryId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    CategoryId = reader.GetInt32(0),
                    CategoryName = reader.GetString(1)
                });
            }

            return categories;
        }

        public void Update(Category category)
        {
            const string sql =
                "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql =
                "DELETE FROM Categories WHERE CategoryId = @CategoryId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
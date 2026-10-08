using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class ProductDAL : IProductDAL
    {
        private readonly string _connectionString;

        public ProductDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Product product)
        {
            const string sql =
                "INSERT INTO Products (ProductName, Price, Quantity, CategoryId) " +
                "VALUES (@ProductName, @Price, @Quantity, @CategoryId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public Product? GetById(int id)
        {
            const string sql =
                "SELECT ProductId, ProductName, Price, Quantity, CategoryId " +
                "FROM Products WHERE ProductId = @ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Product
                {
                    ProductId = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    Quantity = reader.GetInt32(3),
                    CategoryId = reader.GetInt32(4)
                };
            }

            return null;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();

            const string sql =
                "SELECT ProductId, ProductName, Price, Quantity, CategoryId " +
                "FROM Products ORDER BY ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    Quantity = reader.GetInt32(3),
                    CategoryId = reader.GetInt32(4)
                });
            }

            return products;
        }

        public void Update(Product product)
        {
            const string sql =
                "UPDATE Products SET " +
                "ProductName = @ProductName, " +
                "Price = @Price, " +
                "Quantity = @Quantity, " +
                "CategoryId = @CategoryId " +
                "WHERE ProductId = @ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductId", product.ProductId);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql =
                "DELETE FROM Products WHERE ProductId = @ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class NewsProductDAL : INewsProductDAL
    {
        private readonly string _connectionString;

        public NewsProductDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(NewsProduct newsProduct)
        {
            const string sql =
                "INSERT INTO NewsProducts (NewsId, ProductId) " +
                "VALUES (@NewsId, @ProductId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", newsProduct.NewsId);
            command.Parameters.AddWithValue("@ProductId", newsProduct.ProductId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public NewsProduct? GetById(int newsId, int productId)
        {
            const string sql =
                "SELECT NewsId, ProductId " +
                "FROM NewsProducts " +
                "WHERE NewsId = @NewsId AND ProductId = @ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", newsId);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new NewsProduct
                {
                    NewsId = reader.GetInt32(0),
                    ProductId = reader.GetInt32(1)
                };
            }

            return null;
        }

        public List<NewsProduct> GetAll()
        {
            var newsProducts = new List<NewsProduct>();

            const string sql =
                "SELECT NewsId, ProductId " +
                "FROM NewsProducts " +
                "ORDER BY NewsId, ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                newsProducts.Add(new NewsProduct
                {
                    NewsId = reader.GetInt32(0),
                    ProductId = reader.GetInt32(1)
                });
            }

            return newsProducts;
        }

        public void Update(
            NewsProduct newsProduct,
            int oldNewsId,
            int oldProductId)
        {
            const string sql =
                "UPDATE NewsProducts " +
                "SET NewsId = @NewNewsId, ProductId = @NewProductId " +
                "WHERE NewsId = @OldNewsId AND ProductId = @OldProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewNewsId", newsProduct.NewsId);
            command.Parameters.AddWithValue("@NewProductId", newsProduct.ProductId);
            command.Parameters.AddWithValue("@OldNewsId", oldNewsId);
            command.Parameters.AddWithValue("@OldProductId", oldProductId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int newsId, int productId)
        {
            const string sql =
                "DELETE FROM NewsProducts " +
                "WHERE NewsId = @NewsId AND ProductId = @ProductId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", newsId);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
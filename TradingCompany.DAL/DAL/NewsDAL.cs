using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class NewsDAL : INewsDAL
    {
        private readonly string _connectionString;

        public NewsDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(News news)
        {
            const string sql =
                "INSERT INTO News (Title, Content, CreatedAt, IsActive, CreatedByUserId) " +
                "VALUES (@Title, @Content, @CreatedAt, @IsActive, @CreatedByUserId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Title", news.Title);
            command.Parameters.AddWithValue("@Content", news.Content);
            command.Parameters.AddWithValue("@CreatedAt", news.CreatedAt);
            command.Parameters.AddWithValue("@IsActive", news.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserId", news.CreatedByUserId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public News? GetById(int id)
        {
            const string sql =
                "SELECT NewsId, Title, Content, CreatedAt, IsActive, CreatedByUserId " +
                "FROM News WHERE NewsId = @NewsId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new News
                {
                    NewsId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3),
                    IsActive = reader.GetBoolean(4),
                    CreatedByUserId = reader.GetInt32(5)
                };
            }

            return null;
        }

        public List<News> GetAll()
        {
            var newsList = new List<News>();

            const string sql =
                "SELECT NewsId, Title, Content, CreatedAt, IsActive, CreatedByUserId " +
                "FROM News ORDER BY NewsId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                newsList.Add(new News
                {
                    NewsId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3),
                    IsActive = reader.GetBoolean(4),
                    CreatedByUserId = reader.GetInt32(5)
                });
            }

            return newsList;
        }

        public void Update(News news)
        {
            const string sql =
                "UPDATE News SET " +
                "Title = @Title, " +
                "Content = @Content, " +
                "CreatedAt = @CreatedAt, " +
                "IsActive = @IsActive, " +
                "CreatedByUserId = @CreatedByUserId " +
                "WHERE NewsId = @NewsId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", news.NewsId);
            command.Parameters.AddWithValue("@Title", news.Title);
            command.Parameters.AddWithValue("@Content", news.Content);
            command.Parameters.AddWithValue("@CreatedAt", news.CreatedAt);
            command.Parameters.AddWithValue("@IsActive", news.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserId", news.CreatedByUserId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql =
                "DELETE FROM News WHERE NewsId = @NewsId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NewsId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
using Microsoft.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DAL.Models;

namespace TradingCompany.DAL.DAL
{
    public class RoleDAL : IRoleDAL
    {
        private readonly string _connectionString;

        public RoleDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Role role)
        {
            const string sql =
                "INSERT INTO Roles (RoleName) VALUES (@RoleName)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoleName", role.RoleName);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public Role? GetById(int id)
        {
            const string sql =
                "SELECT RoleId, RoleName FROM Roles WHERE RoleId = @RoleId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoleId", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Role
                {
                    RoleId = reader.GetInt32(0),
                    RoleName = reader.GetString(1)
                };
            }

            return null;
        }

        public List<Role> GetAll()
        {
            var roles = new List<Role>();

            const string sql =
                "SELECT RoleId, RoleName FROM Roles ORDER BY RoleId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                roles.Add(new Role
                {
                    RoleId = reader.GetInt32(0),
                    RoleName = reader.GetString(1)
                });
            }

            return roles;
        }

        public void Update(Role role)
        {
            const string sql =
                "UPDATE Roles SET RoleName = @RoleName WHERE RoleId = @RoleId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoleId", role.RoleId);
            command.Parameters.AddWithValue("@RoleName", role.RoleName);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql =
                "DELETE FROM Roles WHERE RoleId = @RoleId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoleId", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
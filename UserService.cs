using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Cloudy_config;
using BCrypt.Net;
namespace Cloudy
{
    internal class UserService
    {
        public async  Task<bool> RegisterUser(string username, string password, string email)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("username/Email/password required");
            }
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            using var Connection = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            await Connection.OpenAsync();
            const string Sql = "INSERT INTO users (username, email, password) VALUES (@u, @e, @p)";

            using var cmd = new NpgsqlCommand(Sql,Connection);
            cmd.Parameters.AddWithValue("u", username);
            cmd.Parameters.AddWithValue("e", email);
            cmd.Parameters.AddWithValue("p", hashed);

            try
            {
                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (PostgresException pex) when (pex.SqlState == "23505") 
            { 
                throw new InvalidOperationException("Username or email already taken", pex);
            }
        }

        public async Task<bool> LoginUser(string usernameOrEmail, string password)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                return false;

            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
            await conn.OpenAsync();

            const string sql = "SELECT password FROM users WHERE username = @u OR email = @u LIMIT 1";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("u", usernameOrEmail);

            var result = await cmd.ExecuteScalarAsync();
            if (result == null) return false;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string hashed = result.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }
    }
}

using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<User> GetAll()
        {
            List<User> users = new();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT UserId, Name, Budget, PreferedRating
                FROM Users;
            ";

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Budget = reader.GetInt32("Budget"),
                    PreferedRating = reader.GetInt32("PreferedRating")
                });
            }

            return users;
        }

        public User? GetById(int id)
        {
            User? user = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT UserId, Name, Budget, PreferedRating
                FROM Users
                WHERE UserId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Budget = reader.GetInt32("Budget"),
                    PreferedRating = reader.GetInt32("PreferedRating")
                };
            }

            return user;
        }

        public User Add(User user)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = @"
                INSERT INTO Users (Name, Budget, PreferedRating)
                VALUES (@name, @budget, @rating);
            ";
            insertCommand.Parameters.AddWithValue("@name", user.Name);
            insertCommand.Parameters.AddWithValue("@budget", user.Budget);
            insertCommand.Parameters.AddWithValue("@rating", user.PreferedRating);

            insertCommand.ExecuteNonQuery();

            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT UserId, Name, Budget, PreferedRating
                FROM Users
                WHERE UserId = LAST_INSERT_ID();
            ";

            using MySqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Budget = reader.GetInt32("Budget"),
                    PreferedRating = reader.GetInt32("PreferedRating")
                };
            }

            throw new Exception("Failed to insert User.");
        }

        public User Update(User user)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand updateCommand = new MySqlCommand();
            updateCommand.Connection = connection;
            updateCommand.CommandText = @"
                UPDATE Users
                SET Name = @name,
                    Budget = @budget,
                    PreferedRating = @rating
                WHERE UserId = @id;
            ";
            updateCommand.Parameters.AddWithValue("@id", user.Id);
            updateCommand.Parameters.AddWithValue("@name", user.Name);
            updateCommand.Parameters.AddWithValue("@budget", user.Budget);
            updateCommand.Parameters.AddWithValue("@rating", user.PreferedRating);

            updateCommand.ExecuteNonQuery();

            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT UserId, Name, Budget, PreferedRating
                FROM Users
                WHERE UserId = @id;
            ";
            selectCommand.Parameters.AddWithValue("@id", user.Id);

            using MySqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Budget = reader.GetInt32("Budget"),
                    PreferedRating = reader.GetInt32("PreferedRating")
                };
            }

            throw new Exception($"User with ID {user.Id} was updated but could not be retrieved.");
        }

        public bool RemoveById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                DELETE FROM Users
                WHERE UserId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}

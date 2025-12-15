using System.Data.Common;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string not found"); 
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT UserId, Name, RestaurantFavorites, Budget, Allergies, PreferedRating 
                FROM Users;
            ";

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    RestaurantFavorites = reader.IsDBNull(reader.GetOrdinal("RestaurantFavorites"))
                        ? new List<int>() : JsonSerializer.Deserialize<List<int>>(reader.GetString("RestaurantFavorites")) ?? new List<int>(),
                    Budget = reader.GetInt32("Budget"),
                    Allergies = reader.IsDBNull(reader.GetOrdinal("Allergies"))
                        ? new List<string>() : JsonSerializer.Deserialize<List<string>>(reader.GetString("Allergies")) ?? new List<string>(),
                    PreferedRating = reader.GetInt32("PreferedRating"),
                });
            }

            return users;
        }

        public User? GetById(int id)
        {
            User? user = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand(@"
                SELECT UserId, Name, RestaurantFavorites, Budget, Allergies, PreferedRating
                FROM User
                WHERE UserId = @id;
            ", connection);

            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),

                    RestaurantFavorites = reader.IsDBNull(reader.GetOrdinal("RestaurantFavorites"))
                        ? new List<int>() : JsonSerializer.Deserialize<List<int>>(reader.GetString("RestaurantFavorites")) ?? new List<int>(),

                    Budget = reader.GetInt32("Budget"),

                    Allergies = reader.IsDBNull(reader.GetOrdinal("Allergies"))
                        ? new List<string>() : JsonSerializer.Deserialize<List<string>>(reader.GetString("Allergies")) ?? new List<string>(),

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
                INSERT INTO User (Name, RestaurantFavorites, Budget, Allergies, PreferedRating)
                VALUES (@name, CAST(@favorites AS JSON), @budget, CAST(@allergies AS JSON), @rating);
            ";

            insertCommand.Parameters.AddWithValue("@name", user.Name);
            insertCommand.Parameters.AddWithValue("@favorites", JsonSerializer.Serialize(user.RestaurantFavorites ?? new List<int>()));
            insertCommand.Parameters.AddWithValue("@budget", user.Budget);
            insertCommand.Parameters.AddWithValue("@allergies", JsonSerializer.Serialize(user.Allergies ?? new List<string>()));
            insertCommand.Parameters.AddWithValue("@rating", user.PreferedRating);

            insertCommand.ExecuteNonQuery();

            // SELECT the inserted row
            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT UserId, Name, RestaurantFavorites, Budget, Allergies, PreferedRating
                FROM User
                WHERE UserId = LAST_INSERT_ID();
            ";

            using MySqlReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("UserId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),

                    RestaurantFavorites = reader.IsDBNull(reader.GetOrdinal("RestaurantFavorites"))
                        ? new List<int>() : JsonSerializer.Deserialize<List<int>>(reader.GetString("RestaurantFavorites")) ?? new List<int>(),

                    Budget = reader.GetInt32("Budget"),

                    Allergies = reader.IsDBNull(reader.GetOrdinal("Allergies"))
                        ? new List<string>() : JsonSerializer.Deserialize<List<string>>(reader.GetString("Allergies")) ?? new List<string>(),

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
                    RestaurantFavorites = CAST(@favorites AS JSON),
                    Budget = @budget,
                    Allergies = CAST(@allergies AS JSON),
                    PreferedRating = @rating
                WHERE UserId = @id;
            ";
            updateCommand.Parameters.AddWithValue("@id", user.Id);
            updateCommand.Parameters.AddWithValue("@name", user.Name);
            updateCommand.Parameters.AddWithValue("@favorites", JsonSerializer.Serialize(user.RestaurantFavorites ?? new List<int>()));
            updateCommand.Parameters.AddWithValue("@budget", user.Budget);
            updateCommand.Parameters.AddWithValue("@allergies", JsonSerializer.Serialize(user.Allergies ?? new List<string>()));
            updateCommand.Parameters.AddWithValue("@rating", user.PreferedRating);

            updateCommand.ExecuteNonQuery();

            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT UserId, Name, RestaurantFavorites, Budget, Allergies, PreferedRating
                From Users
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

                    RestaurantFavorites = reader.IsDBNull(reader.GetOrdinal("RestaurantFavorites"))
                        ? new List<int>() : JsonSerializer.Deserialize<List<int>>(reader.GetString("RestaurantFavorites")) ?? new List<int>(),

                    Budget = reader.GetInt32("Budget"),

                    Allergies = reader.IsDBNull(reader.GetOrdinal("Allergies"))
                        ? new List<string>() : JsonSerializer.Deserialize<List<string>>(reader.GetString("Allergies")) ?? new List<string>(),

                    PreferedRating = reader.GetInt32("PreferedRating"),
                };
            }
            
            throw new Exception($"User with ID {user.Id} was updated but could nit be retrieved. ");
        }

        public bool RemoveById(int id)
        {
            using MySqlConnection connection = new MySqlConnection();
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
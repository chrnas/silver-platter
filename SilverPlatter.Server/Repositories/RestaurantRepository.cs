using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private string _connectionString;

        public RestaurantRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<Restaurant> GetAll()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM Restaurants;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                restaurants.Add(new Restaurant
                {
                    Id = reader.GetInt32("RestaurantId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null: reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null: reader.GetString("Description"),
                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null: reader.GetString("Address")
                });
            }

            connection.Close();
            return restaurants;
        }

        public Restaurant? GetById(int id)
        {
            Restaurant? restaurant = null;
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM Restaurants WHERE RestaurantId = @id;";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                restaurant = new Restaurant
                {
                    Id = reader.GetInt32("RestaurantId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address")
                };
            }

            connection.Close();
            return restaurant;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. INSERT the new restaurant
            using (var insertCommand = new MySqlCommand(@"
                INSERT INTO Restaurants (Name, Description, Address)
                VALUES (@name, @description, @address);
            ", connection))
            {
                insertCommand.Parameters.AddWithValue("@name", restaurant.Name);
                insertCommand.Parameters.AddWithValue("@description", restaurant.Description);
                insertCommand.Parameters.AddWithValue("@address", restaurant.Address);

                insertCommand.ExecuteNonQuery();
            }

            // 2. SELECT the inserted row using LAST_INSERT_ID()
            using (var selectCommand = new MySqlCommand(@"
                SELECT RestaurantId, Name, Description, Address
                FROM Restaurants
                WHERE RestaurantId = LAST_INSERT_ID();
            ", connection))
            {
                using var reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return new Restaurant
                    {
                        Id = reader.GetInt32("RestaurantId"),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address")
                    };
                }
            }

            throw new Exception("Failed to insert restaurant.");
        }


        public Restaurant Update(Restaurant restaurant)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. Perform the UPDATE
            using (var updateCommand = new MySqlCommand(@"
                UPDATE Restaurants
                SET Name = @name,
                    Description = @description,
                    Address = @address
                WHERE RestaurantId = @id;
            ", connection))
            {
                updateCommand.Parameters.AddWithValue("@id", restaurant.Id);
                updateCommand.Parameters.AddWithValue("@name", restaurant.Name);
                updateCommand.Parameters.AddWithValue("@description", restaurant.Description);
                updateCommand.Parameters.AddWithValue("@address", restaurant.Address);

                updateCommand.ExecuteNonQuery();
            }

            // 2. SELECT the updated row
            using (var selectCommand = new MySqlCommand(@"
                SELECT RestaurantId, Name, Description, Address
                FROM Restaurants
                WHERE RestaurantId = @id;
            ", connection))
            {
                selectCommand.Parameters.AddWithValue("@id", restaurant.Id);

                using var reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return new Restaurant
                    {
                        Id = reader.GetInt32("RestaurantId"),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address")
                    };
                }
            }
            throw new Exception($"Restaurant with ID {restaurant.Id} was updated but could not be retrieved.");
        }



        public void RemoveById(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"DELETE FROM Restaurants WHERE RestaurantId = @id;";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

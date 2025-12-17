using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class RestaurantFavoriteRepository : IRestaurantFavoriteRepository
    {
        private readonly string _connectionString;

        public RestaurantFavoriteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<RestaurantFavorite> GetAll()
        {
            List<RestaurantFavorite> favorites = new();

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"SELECT RestaurantFavoritesId, UserId, RestaurantId FROM RestaurantFavorites;"
            };

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                favorites.Add(new RestaurantFavorite
                {
                    Id = reader.GetInt32("RestaurantFavoritesId"),
                    UserId = reader.GetInt32("UserId"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                });
            }

            return favorites;
        }

        public List<RestaurantFavorite> GetByUserId(int userId)
        {
            List<RestaurantFavorite> favorites = new();

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT RestaurantFavoritesId, UserId, RestaurantId
                    FROM RestaurantFavorites
                    WHERE UserId = @userId;"
            };

            command.Parameters.AddWithValue("@userId", userId);

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                favorites.Add(new RestaurantFavorite
                {
                    Id = reader.GetInt32("RestaurantFavoritesId"),
                    UserId = reader.GetInt32("UserId"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                });
            }

            return favorites;
        }

        public RestaurantFavorite? GetById(int id)
        {
            RestaurantFavorite? favorite = null;

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT RestaurantFavoritesId, UserId, RestaurantId
                    FROM RestaurantFavorites
                    WHERE RestaurantFavoritesId = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                favorite = new RestaurantFavorite
                {
                    Id = reader.GetInt32("RestaurantFavoritesId"),
                    UserId = reader.GetInt32("UserId"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            return favorite;
        }

        public RestaurantFavorite Add(RestaurantFavorite favorite)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand insert = new()
            {
                Connection = connection,
                CommandText = @"
                    INSERT INTO RestaurantFavorites (UserId, RestaurantId)
                    VALUES (@userId, @restaurantId);"
            };

            insert.Parameters.AddWithValue("@userId", favorite.UserId);
            insert.Parameters.AddWithValue("@restaurantId", favorite.RestaurantId);

            insert.ExecuteNonQuery();

            using MySqlCommand select = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT RestaurantFavoritesId, UserId, RestaurantId
                    FROM RestaurantFavorites
                    WHERE RestaurantFavoritesId = LAST_INSERT_ID();"
            };

            using MySqlDataReader reader = select.ExecuteReader();
            if (reader.Read())
            {
                return new RestaurantFavorite
                {
                    Id = reader.GetInt32("RestaurantFavoritesId"),
                    UserId = reader.GetInt32("UserId"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            throw new Exception("Failed to insert RestaurantFavorite.");
        }

        public bool RemoveById(int id)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"DELETE FROM RestaurantFavorites WHERE RestaurantFavoritesId = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool RemoveByUserAndRestaurant(int userId, int restaurantId)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"
                    DELETE FROM RestaurantFavorites 
                    WHERE UserId = @userId AND RestaurantId = @restaurantId;"
            };

            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@restaurantId", restaurantId);

            return command.ExecuteNonQuery() > 0;
        }
    }
}

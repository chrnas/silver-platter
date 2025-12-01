using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class MenuEntryRepository : IMenuEntryRepository
    {
        private string _connectionString;

        public MenuEntryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<MenuEntry> GetAll()
        {
            List<MenuEntry> menuEntries = new List<MenuEntry>();
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM MenuEntries;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                menuEntries.Add(new MenuEntry
                {
                    Id = reader.GetInt32("MenuEntryId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    RestaurantId = reader.GetInt32("RestaurantId"),
                });
            }

            connection.Close();
            return menuEntries;
        }

        public MenuEntry? GetById(int id)
        {
            MenuEntry? menuEntry = null;
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM MenuEntries WHERE MenuEntryId = @id;";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                menuEntry = new MenuEntry
                {
                    Id = reader.GetInt32("MenuEntryId"),
                    Name = reader.GetString("Name"),
                    Description = reader.GetString("Description"),
                    RestaurantId = reader.GetInt32("RestaurantId"),
                };
            }

            connection.Close();
            return menuEntry;
        }

        public MenuEntry Add(MenuEntry entry)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. INSERT new menu entry
            using (var insertCommand = new MySqlCommand(@"
                INSERT INTO MenuEntries (Name, Description, RestaurantId)
                VALUES (@name, @description, @restaurantId);
            ", connection))
            {
                insertCommand.Parameters.AddWithValue("@name", entry.Name);
                insertCommand.Parameters.AddWithValue("@description", entry.Description);
                insertCommand.Parameters.AddWithValue("@restaurantId", entry.RestaurantId);

                insertCommand.ExecuteNonQuery();
            }

            // 2. SELECT the inserted row using LAST_INSERT_ID()
            using (var selectCommand = new MySqlCommand(@"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries
                WHERE MenuEntryId = LAST_INSERT_ID();
            ", connection))
            {
                using var reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return new MenuEntry
                    {
                        Id = reader.GetInt32("MenuEntryId"),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                        RestaurantId = reader.GetInt32("RestaurantId")
                    };
                }
            }

            throw new Exception("Failed to insert MenuEntry.");
        }


        public MenuEntry Update(MenuEntry entry)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. UPDATE
            using (var updateCommand = new MySqlCommand(@"
                UPDATE MenuEntries
                SET Name = @name,
                    Description = @description,
                    RestaurantId = @restaurantId
                WHERE MenuEntryId = @id;
            ", connection))
            {
                updateCommand.Parameters.AddWithValue("@id", entry.Id);
                updateCommand.Parameters.AddWithValue("@name", entry.Name);
                updateCommand.Parameters.AddWithValue("@description", entry.Description);
                updateCommand.Parameters.AddWithValue("@restaurantId", entry.RestaurantId);

                updateCommand.ExecuteNonQuery();
            }

            // 2. SELECT updated row
            using (var selectCommand = new MySqlCommand(@"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries
                WHERE MenuEntryId = @id;
            ", connection))
            {
                selectCommand.Parameters.AddWithValue("@id", entry.Id);

                using var reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return new MenuEntry
                    {
                        Id = reader.GetInt32("MenuEntryId"),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                        RestaurantId = reader.GetInt32("RestaurantId")
                    };
                }
            }

            throw new Exception($"MenuEntry with ID {entry.Id} was updated but could not be retrieved.");
        }

        public void RemoveById(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"DELETE FROM MenuEntries WHERE MenuEntryId = @id;";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

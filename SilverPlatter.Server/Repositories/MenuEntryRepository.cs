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

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries;
            ";

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                menuEntries.Add(new MenuEntry
                {
                    Id = reader.GetInt32("MenuEntryId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                });
            }

            return menuEntries;
        }

        public MenuEntry? GetById(int id)
        {
            MenuEntry? menuEntry = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries
                WHERE MenuEntryId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                menuEntry = new MenuEntry
                {
                    Id = reader.GetInt32("MenuEntryId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            return menuEntry;
        }

        public MenuEntry Add(MenuEntry entry)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. INSERT the new menu entry
            using MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = @"
                INSERT INTO MenuEntries (Name, Description, RestaurantId)
                VALUES (@name, @description, @restaurantId);
            ";
            insertCommand.Parameters.AddWithValue("@name", entry.Name);
            insertCommand.Parameters.AddWithValue("@description", entry.Description);
            insertCommand.Parameters.AddWithValue("@restaurantId", entry.RestaurantId);

            insertCommand.ExecuteNonQuery();

            // 2. SELECT the inserted row using LAST_INSERT_ID()
            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries
                WHERE MenuEntryId = LAST_INSERT_ID();
            ";

            using MySqlDataReader reader = selectCommand.ExecuteReader();
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

            throw new Exception("Failed to insert MenuEntry.");
        }

        public MenuEntry Update(MenuEntry entry)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. UPDATE
            using MySqlCommand updateCommand = new MySqlCommand();
            updateCommand.Connection = connection;
            updateCommand.CommandText = @"
                UPDATE MenuEntries
                SET Name = @name,
                    Description = @description,
                    RestaurantId = @restaurantId
                WHERE MenuEntryId = @id;
            ";
            updateCommand.Parameters.AddWithValue("@id", entry.Id);
            updateCommand.Parameters.AddWithValue("@name", entry.Name);
            updateCommand.Parameters.AddWithValue("@description", entry.Description);
            updateCommand.Parameters.AddWithValue("@restaurantId", entry.RestaurantId);

            updateCommand.ExecuteNonQuery();

            // 2. SELECT updated row
            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT MenuEntryId, Name, Description, RestaurantId
                FROM MenuEntries
                WHERE MenuEntryId = @id;
            ";
            selectCommand.Parameters.AddWithValue("@id", entry.Id);

            using MySqlDataReader reader = selectCommand.ExecuteReader();
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

            throw new Exception($"MenuEntry with ID {entry.Id} was updated but could not be retrieved.");
        }

        public void RemoveById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                DELETE FROM MenuEntries 
                WHERE MenuEntryId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}

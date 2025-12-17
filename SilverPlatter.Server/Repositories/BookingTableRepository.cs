using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class BookingTableRepository : IBookingTableRepository
    {
        private string _connectionString;

        public BookingTableRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<BookingTable> GetAll()
        {
            List<BookingTable> tables = new List<BookingTable>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables;
            ";

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(new BookingTable
                {
                    Id = reader.GetInt32("BookingTableId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Places = reader.GetInt32("Places"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                });
            }

            return tables;
        }

        public BookingTable? GetById(int id)
        {
            BookingTable? table = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE BookingTableId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                table = new BookingTable
                {
                    Id = reader.GetInt32("BookingTableId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Places = reader.GetInt32("Places"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            return table;
        }

        public BookingTable? GetByRestaurantId(int id)
        {
            BookingTable? table = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE RestaurantId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                table = new BookingTable
                {
                    Id = reader.GetInt32("BookingTableId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Places = reader.GetInt32("Places"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            return table;
        }

        public BookingTable Add(BookingTable table)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. INSERT new booking table
            using MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = @"
                INSERT INTO BookingTables (Name, Description, Places, RestaurantId)
                VALUES (@name, @description, @places, @restaurantId);
            ";
            insertCommand.Parameters.AddWithValue("@name", table.Name);
            insertCommand.Parameters.AddWithValue("@description", table.Description);
            insertCommand.Parameters.AddWithValue("@places", table.Places);
            insertCommand.Parameters.AddWithValue("@restaurantId", table.RestaurantId);

            insertCommand.ExecuteNonQuery();

            // 2. SELECT the inserted row using LAST_INSERT_ID()
            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE BookingTableId = LAST_INSERT_ID();
            ";

            using MySqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                return new BookingTable
                {
                    Id = reader.GetInt32("BookingTableId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Places = reader.GetInt32("Places"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            throw new Exception("Failed to insert BookingTable.");
        }

        public BookingTable Update(BookingTable table)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. UPDATE
            using MySqlCommand updateCommand = new MySqlCommand();
            updateCommand.Connection = connection;
            updateCommand.CommandText = @"
                UPDATE BookingTables
                SET Name = @name,
                    Description = @description,
                    Places = @places,
                    RestaurantId = @restaurantId
                WHERE BookingTableId = @id;
            ";
            updateCommand.Parameters.AddWithValue("@id", table.Id);
            updateCommand.Parameters.AddWithValue("@name", table.Name);
            updateCommand.Parameters.AddWithValue("@description", table.Description);
            updateCommand.Parameters.AddWithValue("@places", table.Places);
            updateCommand.Parameters.AddWithValue("@restaurantId", table.RestaurantId);

            updateCommand.ExecuteNonQuery();

            // 2. SELECT updated row
            using MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = connection;
            selectCommand.CommandText = @"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE BookingTableId = @id;
            ";
            selectCommand.Parameters.AddWithValue("@id", table.Id);

            using MySqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                return new BookingTable
                {
                    Id = reader.GetInt32("BookingTableId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Places = reader.GetInt32("Places"),
                    RestaurantId = reader.GetInt32("RestaurantId")
                };
            }

            throw new Exception($"BookingTable with ID {table.Id} was updated but could not be retrieved.");
        }

        public bool RemoveById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"
                DELETE FROM BookingTables
                WHERE BookingTableId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;// return true if at least one row was deleted
        }
    }
}

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
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM BookingTables;";

            using var reader = command.ExecuteReader();
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

            connection.Close();
            return tables;
        }

        public BookingTable? GetById(int id)
        {
            BookingTable? table = null;
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT * FROM Bookingtables WHERE BookingTableId = @id;";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
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

            connection.Close();
            return table;
        }

        public BookingTable Add(BookingTable table)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. INSERT new booking table
            using (var insertCommand = new MySqlCommand(@"
                INSERT INTO BookingTables (Name, Description, Places, RestaurantId)
                VALUES (@name, @description, @places, @restaurantId);
            ", connection))
            {
                insertCommand.Parameters.AddWithValue("@name", table.Name);
                insertCommand.Parameters.AddWithValue("@description", table.Description);
                insertCommand.Parameters.AddWithValue("@places", table.Places);
                insertCommand.Parameters.AddWithValue("@restaurantId", table.RestaurantId);

                insertCommand.ExecuteNonQuery();
            }

            // 2. SELECT the inserted row using LAST_INSERT_ID()
            using (var selectCommand = new MySqlCommand(@"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE BookingTableId = LAST_INSERT_ID();
            ", connection))
            {
                using var reader = selectCommand.ExecuteReader();
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
            }

            throw new Exception("Failed to insert BookingTable.");
        }


        public BookingTable Update(BookingTable table)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // 1. Update the table
            using (var updateCommand = new MySqlCommand(@"
                UPDATE BookingTables
                SET Name = @name,
                    Description = @description,
                    Places = @places,
                    RestaurantId = @restaurantId
                WHERE BookingTableId = @id;
            ", connection))
            {
                updateCommand.Parameters.AddWithValue("@id", table.Id);
                updateCommand.Parameters.AddWithValue("@name", table.Name);
                updateCommand.Parameters.AddWithValue("@description", table.Description);
                updateCommand.Parameters.AddWithValue("@places", table.Places);
                updateCommand.Parameters.AddWithValue("@restaurantId", table.RestaurantId);

                updateCommand.ExecuteNonQuery();
            }

            // 2. Select updated row
            using (var selectCommand = new MySqlCommand(@"
                SELECT BookingTableId, Name, Description, Places, RestaurantId
                FROM BookingTables
                WHERE BookingTableId = @id;
            ", connection))
            {
                selectCommand.Parameters.AddWithValue("@id", table.Id);

                using var reader = selectCommand.ExecuteReader();
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
            }

            throw new Exception($"BookingTable with ID {table.Id} was updated but could not be retrieved.");
        }


        public void RemoveById(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = @"DELETE FROM BookingTables WHERE BookingTableId = @id;";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

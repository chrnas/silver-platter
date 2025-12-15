using MySql.Data.MySqlClient;
using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly string _connectionString;

        public AllergyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public List<Allergy> GetAll()
        {
            List<Allergy> allergies = new();

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"SELECT AllergiesId, Name, UserId FROM Allergies;"
            };

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                allergies.Add(new Allergy
                {
                    Id = reader.GetInt32("AllergiesId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    UserId = reader.GetInt32("UserId")
                });
            }

            return allergies;
        }

        public List<Allergy> GetByUserId(int userId)
        {
            List<Allergy> allergies = new();

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT AllergiesId, Name, UserId
                    FROM Allergies
                    WHERE UserId = @userId;"
            };

            command.Parameters.AddWithValue("@userId", userId);

            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                allergies.Add(new Allergy
                {
                    Id = reader.GetInt32("AllergiesId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    UserId = reader.GetInt32("UserId")
                });
            }

            return allergies;
        }

        public Allergy? GetById(int id)
        {
            Allergy? allergy = null;

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT AllergiesId, Name, UserId
                    FROM Allergies
                    WHERE AllergiesId = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                allergy = new Allergy
                {
                    Id = reader.GetInt32("AllergiesId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    UserId = reader.GetInt32("UserId")
                };
            }

            return allergy;
        }

        public Allergy Add(Allergy allergy)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand insert = new()
            {
                Connection = connection,
                CommandText = @"
                    INSERT INTO Allergies (Name, UserId)
                    VALUES (@name, @userId);"
            };

            insert.Parameters.AddWithValue("@name", allergy.Name);
            insert.Parameters.AddWithValue("@userId", allergy.UserId);

            insert.ExecuteNonQuery();

            using MySqlCommand select = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT AllergiesId, Name, UserId
                    FROM Allergies
                    WHERE AllergiesId = LAST_INSERT_ID();"
            };

            using MySqlDataReader reader = select.ExecuteReader();
            if (reader.Read())
            {
                return new Allergy
                {
                    Id = reader.GetInt32("AllergiesId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    UserId = reader.GetInt32("UserId")
                };
            }

            throw new Exception("Failed to insert Allergy.");
        }

        public Allergy Update(Allergy allergy)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand update = new()
            {
                Connection = connection,
                CommandText = @"
                    UPDATE Allergies
                    SET Name = @name,
                        UserId = @userId
                    WHERE AllergiesId = @id;"
            };

            update.Parameters.AddWithValue("@id", allergy.Id);
            update.Parameters.AddWithValue("@name", allergy.Name);
            update.Parameters.AddWithValue("@userId", allergy.UserId);

            update.ExecuteNonQuery();

            using MySqlCommand select = new()
            {
                Connection = connection,
                CommandText = @"
                    SELECT AllergiesId, Name, UserId
                    FROM Allergies
                    WHERE AllergiesId = @id;"
            };

            select.Parameters.AddWithValue("@id", allergy.Id);

            using MySqlDataReader reader = select.ExecuteReader();
            if (reader.Read())
            {
                return new Allergy
                {
                    Id = reader.GetInt32("AllergiesId"),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString("Name"),
                    UserId = reader.GetInt32("UserId")
                };
            }

            throw new Exception($"Allergy with ID {allergy.Id} was updated but not returned.");
        }

        public bool RemoveById(int id)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new()
            {
                Connection = connection,
                CommandText = @"DELETE FROM Allergies WHERE AllergiesId = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() > 0;
        }
    }
}
